using FluentValidation;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Application.Common.Validators.Workouts
{
    public class WorkoutValidator : AbstractValidator<Workout>
    {
        public WorkoutValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
                .Matches("^[a-zA-Z]+( [a-zA-Z]+)*$");
            RuleFor(x => x.LevelId).NotEmpty().WithMessage("LevelId is required.");
            RuleFor(x => x.GoalId).NotEmpty().WithMessage("GoalId is required.");
        }
    }
}
