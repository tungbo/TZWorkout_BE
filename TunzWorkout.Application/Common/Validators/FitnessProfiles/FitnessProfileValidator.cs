using FluentValidation;
using TunzWorkout.Domain.Entities.FitnessProfiles;

namespace TunzWorkout.Application.Common.Validators.FitnessProfiles
{
    public class FitnessProfileValidator : AbstractValidator<FitnessProfile>
    {
        public FitnessProfileValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(40).LessThanOrEqualTo(150);
            RuleFor(x => x.Height).GreaterThanOrEqualTo(132).LessThanOrEqualTo(228);
        }
    }
}
