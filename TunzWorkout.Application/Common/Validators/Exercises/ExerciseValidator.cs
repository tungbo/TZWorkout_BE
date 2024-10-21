
using FluentValidation;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Application.Common.Validators.Exercises
{
    public class ExerciseValidator : AbstractValidator<Exercise>
    {
        public ExerciseValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
