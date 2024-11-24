using FluentValidation;
using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Application.Common.Validators.Rounds
{
    public class RoundValidator : AbstractValidator<Round>
    {
        public RoundValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");
            RuleFor(x => x.Set)
                .NotEmpty().WithMessage("Set is required.")
                .InclusiveBetween(0, 50).WithMessage("Set must be between 0 and 50.");
            RuleFor(x => x.Rest).InclusiveBetween(0, 30).WithMessage("Rest must be between 0 and 30.");
            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.")
                .InclusiveBetween(0, 10).WithMessage("Order must be between 0 and 10.");
        }
    }
}
