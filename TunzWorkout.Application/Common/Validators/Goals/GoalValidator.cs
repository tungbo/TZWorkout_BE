

using FluentValidation;
using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Application.Common.Validators.Goals
{
    public class GoalValidator : AbstractValidator<Goal>
    {
        public GoalValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}
