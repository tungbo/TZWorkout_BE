using FluentValidation;
using TunzWorkout.Domain.Entities.RoundExercises;

namespace TunzWorkout.Application.Common.Validators.RoundExercises
{
    public class RoundExerciseValidator : AbstractValidator<RoundExercise>
    {
        public RoundExerciseValidator()
        {
            RuleFor(x => x.RoundId)
                .NotEmpty().WithMessage("RoundId is required.");
            RuleFor(x => x.ExerciseId).NotEmpty().WithMessage("ExerciseId is required.");
            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order is required.")
                .InclusiveBetween(0, 10).WithMessage("Order must be between 0 and 10.");
            RuleFor(x => x.Reps).NotEmpty().WithMessage("Reps is required.")
                .InclusiveBetween(0, 100).WithMessage("Reps must be between 0 and 100.");
            RuleFor(x => x.Rest).InclusiveBetween(0, 360).WithMessage("Rest must be between 0 and 360.");
        }
    }
}
