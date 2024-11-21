using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Application.Common.Services.Goals
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Goal> _goalValidator;
        public GoalService(IGoalRepository goalRepository, IUnitOfWork unitOfWork, IValidator<Goal> goalValidator)
        {
            _goalRepository = goalRepository;
            _unitOfWork = unitOfWork;
            _goalValidator = goalValidator;
        }
        public async Task<ErrorOr<Goal>> CreateAsync(Goal goal)
        {
            var validationResult = await _goalValidator.ValidateAsync(goal);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            if (await _goalRepository.ExistByNameAsync(goal.Name))
            {
                return Error.Conflict(description: "Goal exist");
            }

            await _goalRepository.CreateAsync(goal);
            await _unitOfWork.CommitChangesAsync();

            return goal;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            if(!await _goalRepository.ExistByIdAsync(id))
            {
                return Error.Conflict(description: "Goal not found");
            }

            await _goalRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<IEnumerable<Goal>>> GetAllAsync()
        {
            var goals = await _goalRepository.GetAllAsync();
            if(goals is null || !goals.Any())
            {
                return Error.NotFound(description: "No goals found.");
            }

            return goals.ToList();
        }

        public async Task<ErrorOr<Goal>> GetGoalByIdAsync(Guid id)
        {
            var goal = await _goalRepository.GoalByIdAsync(id);
            if (goal is null)
            {
                return Error.NotFound(description: "Goal not found");
            }
            return goal;
        }

        public async Task<ErrorOr<Goal>> UpdateAsync(Goal goal)
        {
            var validationResult = await _goalValidator.ValidateAsync(goal);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var existingGoal = await _goalRepository.GoalByIdAsync(goal.Id);
            if (existingGoal is null)
            {
                return Error.NotFound(description: "Goal not found");
            }
            var goalWithSameName = await _goalRepository.GoalByNameAsync(goal.Name);
            if (goalWithSameName is not null && goalWithSameName.Id != goal.Id)
            {
                return Error.Conflict(description: "Goal with same name exist");
            }

            await _goalRepository.UpdateAsync(goal);
            await _unitOfWork.CommitChangesAsync();
            return goal;
        }
    }
}
