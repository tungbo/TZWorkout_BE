using ErrorOr;
using FluentValidation;
using System.Security.Claims;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.UserGoals;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Application.Common.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFitnessProfileRepository _fitnessProfileRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterCommand> _registerValidator;
        private readonly IValidator<LoginCommand> _loginValidator;
        public AuthenticationService(IUserRepository userRepository, IFitnessProfileRepository fitnessProfileRepository, IGoalRepository goalRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IUnitOfWork unitOfWork, IValidator<RegisterCommand> registerValidator, IValidator<LoginCommand> loginValidator)
        {
            _userRepository = userRepository;
            _fitnessProfileRepository = fitnessProfileRepository;
            _goalRepository = goalRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }
        public async Task<ErrorOr<AuthenticationCommand>> RegisterAsync(RegisterCommand registerCommand)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerCommand);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            if (await _userRepository.ExistByEmailAsync(registerCommand.Email))
            {
                return Error.Conflict(description: "Email already exists");
            }
            if (!Enum.IsDefined(typeof(Gender), registerCommand.Gender))
            {
                return Error.Validation(code: nameof(registerCommand.Gender), description: "Invalid gender value");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerCommand.Username,
                Email = registerCommand.Email,
                FirstName = registerCommand.FirstName,
                LastName = registerCommand.LastName,
                PasswordHash = _passwordHasher.HashPassword(registerCommand.Password),
                CreateAt = DateTime.Now,
                Role = UserRole.User,
                IsActive = true,
                IsDeleted = false
            };
            var fitnessProfile = new FitnessProfile
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                LevelId = registerCommand.LevelId,
                Gender = registerCommand.Gender,
                Height = registerCommand.Height,
                Weight = registerCommand.Weight,
            };

            foreach(var goalId in registerCommand.SelectedGoalIds)
            {
                var goalExists = await _goalRepository.ExistByIdAsync(goalId);
                if(goalExists)
                {
                    var userGoal = new UserGoal
                    {
                        FitnessProfileId = fitnessProfile.Id,
                        GoalId = goalId
                    };
                    fitnessProfile.UserGoals.Add(userGoal);
                }
                else
                {
                    return Error.NotFound(description: "Goal not found");
                }
            }


            await _userRepository.CreateAsync(user);
            await _fitnessProfileRepository.CreateAsync(fitnessProfile);
            await _unitOfWork.CommitChangesAsync();

            var authenticationCommand = _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationCommand.RefreshToken;
            user.RefreshTokenExpiry = authenticationCommand.RefreshTokenExpiry;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync();
            return authenticationCommand;
        }

        public async Task<ErrorOr<AuthenticationCommand>> LoginAsync(LoginCommand loginCommand)
        {
            var validationResult = _loginValidator.Validate(loginCommand);
            if(!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            var user = await _userRepository.GetByEmailAsync(loginCommand.Email);
            if(user is null)
            {
                return Error.NotFound(description: "Email not registered");
            }

            if (!_passwordHasher.IsCorrectPassword(loginCommand.Password, user.PasswordHash))
            {
                return Error.Unauthorized(description: "Invalid password");
            }

            var authenticationCommand = _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationCommand.RefreshToken;
            user.RefreshTokenExpiry = authenticationCommand.RefreshTokenExpiry;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync();
            return authenticationCommand;
        }

        public async Task<ErrorOr<Success>> LogoutAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
            {
                return Error.NotFound(description: "User not found");
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiry = DateTime.MinValue;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync();
            return Result.Success;
        }

        public async Task<ErrorOr<AuthenticationCommand>> GenerateNewAccessToken(TokenModelCommand tokenModelCommand)
        {
            if(tokenModelCommand is null)
            {
                return Error.NotFound(description: "Token  not found");
            }

            string jwtToken = tokenModelCommand.Token;
            string refreshToken = tokenModelCommand.RefreshToken;

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(jwtToken);
            if (principal is null)
            {
                return Error.Conflict(description: "Invalid refresh token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.Now)
            {
                return Error.Conflict(description: "Invalid refresh token");
            }

            AuthenticationCommand authenticationCommand = _jwtService.CreateJwtToken(user);
            user.RefreshToken = authenticationCommand.RefreshToken;
            user.RefreshTokenExpiry = authenticationCommand.RefreshTokenExpiry;

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitChangesAsync();

            return authenticationCommand;
        }
    }
}
