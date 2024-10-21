using FluentValidation;
using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Application.Common.Validators.Levels
{
    public class LevelValidator : AbstractValidator<Level>
    {
        public LevelValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Length(2, 20).Matches("^[a-zA-Z]+( [a-zA-Z]+)*$");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        }
    }
}
