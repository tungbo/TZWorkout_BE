using FluentValidation;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Validators.Muscles
{
    public class MuscleValidator : AbstractValidator<Muscle>
    {
        public MuscleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Matches(@"^[^\d]+$");
        }
    }
}
