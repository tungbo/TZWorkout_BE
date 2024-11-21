using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Authentication;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Application.Common.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<RegisterCommand> _registerValidator;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<User> userValidator, IValidator<RegisterCommand> registerValidator, IJwtService jwtService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userValidator = userValidator;
            _registerValidator = registerValidator;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }
        public async Task<ErrorOr<AuthenticationCommand>> CreateAsync(RegisterCommand registerCommand)
        {
            //var validationResult = await _registerValidator.ValidateAsync(registerCommand);
            //if(!validationResult.IsValid)
            //{
            //    return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            //}

            //if (await _userRepository.ExistByEmailAsync(registerCommand.Email))
            //{
            //    return Error.Conflict(description: "Email already exists");
            //}

            //var user = new User
            //{
            //    Id = Guid.NewGuid(),
            //    Username = registerCommand.Username,
            //    Email = registerCommand.Email,
            //    FirstName = registerCommand.FirstName,
            //    LastName = registerCommand.LastName,
            //    PasswordHash = _passwordHasher.HashPassword(registerCommand.Password),
            //    CreateAt = DateTime.Now,
            //    Role = UserRole.User,
            //    IsActive = true,
            //    IsDeleted = false
            //};

            //await _userRepository.CreateAsync(user);
            //await _unitOfWork.CommitChangesAsync();

            //var authenticationCommand = _jwtService.CreateJwtToken(user);
            //user.RefreshToken = authenticationCommand.RefreshToken;
            //user.RefreshTokenExpiry = authenticationCommand.RefreshTokenExpiry;

            //await _userRepository.UpdateAsync(user);
            //await _unitOfWork.CommitChangesAsync();
            //return authenticationCommand;

            throw new NotImplementedException();
        }

        public async Task<ErrorOr<IEnumerable<User>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if(users is null || !users.Any())
            {
                return Error.NotFound(description: "No users found");
            }
            return users.ToList();
        }

        public Task<ErrorOr<User?>> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<User?>> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return Error.NotFound(description: "User not found");
            }
            return user;
        }

        public async Task<ErrorOr<User>> UpdateAsync(User user)
        {
            var validationResult = await _userValidator.ValidateAsync(user);
            if(!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser is null)
            {
                return Error.NotFound(description: "User not found");
            }

            existingUser.Username = user.Username;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;
            existingUser.IsActive = user.IsActive;
            existingUser.IsDeleted = user.IsDeleted;

            await _userRepository.UpdateAsync(existingUser);
            await _unitOfWork.CommitChangesAsync();

            return existingUser;

        }
    }
}
