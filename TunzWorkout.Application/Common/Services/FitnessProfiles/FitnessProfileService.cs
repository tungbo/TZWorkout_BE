using ErrorOr;
using FluentValidation;
using System.Linq;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.UserGoals;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Application.Common.Services.FitnessProfiles
{
    public class FitnessProfileService : IFitnessProfileService
    {
        private readonly IFitnessProfileRepository _fitnessProfileRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IUserGoalRepository _userGoalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<FitnessProfile> _fitnessProfileValidator;
        public FitnessProfileService(IFitnessProfileRepository fitnessProfileRepository, IUserGoalRepository userGoalRepository, ILevelRepository levelRepository, IGoalRepository goalRepository, IUnitOfWork unitOfWork, IValidator<FitnessProfile> fitnessProfileValidator)
        {
            _fitnessProfileRepository = fitnessProfileRepository;
            _userGoalRepository = userGoalRepository;
            _levelRepository = levelRepository;
            _goalRepository = goalRepository;
            _unitOfWork = unitOfWork;
            _fitnessProfileValidator = fitnessProfileValidator;
        }
        public async Task<ErrorOr<FitnessProfile>> CreateAsync(FitnessProfile fitnessProfile)
        {
            //var validationResult = await _fitnessProfileValidator.ValidateAsync(fitnessProfile);
            //if(!validationResult.IsValid)
            //{
            //    return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            //}

            //await _fitnessProfileRepository.CreateAsync(fitnessProfile);
            //await _unitOfWork.CommitChangesAsync();
            //return fitnessProfile;
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<IEnumerable<FitnessProfile>>> GetAllAsync()
        {
            var fitnessProfiles = await _fitnessProfileRepository.GetAllAsync();
            if(fitnessProfiles is null || !fitnessProfiles.Any())
            {
                return Error.NotFound(description: "No fitness profiles found");
            }
            return fitnessProfiles.ToList();
        }

        public Task<ErrorOr<FitnessProfile>> GetFitnessProfileByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<FitnessProfile>> GetFitnessProfileByUserIdAsync(Guid userId)
        {
            var fitnessProfile = await _fitnessProfileRepository.FitnessProfileByUserId(userId);
            if(fitnessProfile is null)
            {
                return Error.NotFound(description: "Fitness profile not found");
            }
            return fitnessProfile;
        }

        public async Task<ErrorOr<FitnessProfile>> UpdateAsync(FitnessProfile fitnessProfile)
        {
            var validationResult = await _fitnessProfileValidator.ValidateAsync(fitnessProfile);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var levelById = await _levelRepository.ExistByIdAsync(fitnessProfile.LevelId);
            if (!levelById)
            {
                return Error.NotFound(description: "Level not found");
            }
            if (!Enum.IsDefined(typeof(Gender), fitnessProfile.Gender))
            {
                return Error.NotFound(description: "Invalid gender");
            }
            var existingFitnessProfile = await _fitnessProfileRepository.FitnessProfileByUserId(fitnessProfile.UserId);

            if (fitnessProfile.SelectedGoalIds.Any())
            {
                foreach (var goalId in fitnessProfile.SelectedGoalIds)
                {
                    if (!await _goalRepository.ExistByIdAsync(goalId))
                    {
                        return Error.NotFound(description: "Goal not found");
                    }
                }
            }
            await UpdateUserGoal(fitnessProfile);

            existingFitnessProfile.LevelId = fitnessProfile.LevelId;
            existingFitnessProfile.Gender = fitnessProfile.Gender;
            existingFitnessProfile.Height = fitnessProfile.Height;
            existingFitnessProfile.Weight = fitnessProfile.Weight;

            await _fitnessProfileRepository.UpdateAsync(fitnessProfile);
            await _unitOfWork.CommitChangesAsync();

            var updatedFitnessProfile = await _fitnessProfileRepository.FitnessProfileByUserId(fitnessProfile.UserId);
            return updatedFitnessProfile is not null ? updatedFitnessProfile : Error.NotFound(description: "FitnessProfile not found");
        }

        private async Task UpdateUserGoal(FitnessProfile fitnessProfile)
        {
            var existingGoal = await _userGoalRepository.GetByFitnessProfileId(fitnessProfile.Id);
            var existingGoalIds = existingGoal.Select(eg => eg.GoalId).ToList();

            var goalsToRemove = existingGoal.Where(eg => !fitnessProfile.SelectedGoalIds.Contains(eg.GoalId)).ToList();
            if (goalsToRemove.Any())
            {
                await _userGoalRepository.DeleteRangeAsync(goalsToRemove);
                await _unitOfWork.CommitChangesAsync();
            }

            var newGoalIds = fitnessProfile.SelectedGoalIds.Except(existingGoalIds).ToList();
            var goalToAdd = newGoalIds.Select(goalId => new UserGoal 
            {
                FitnessProfileId = fitnessProfile.Id,
                GoalId = goalId
            }).ToList();

            if(goalToAdd.Any())
            {
                await _userGoalRepository.AddRangeAsync(goalToAdd);
            }
        }
    }
}
