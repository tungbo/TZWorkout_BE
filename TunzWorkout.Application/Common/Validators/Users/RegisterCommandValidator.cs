using FluentValidation;
using TunzWorkout.Application.Commands.Authentication;

namespace TunzWorkout.Application.Common.Validators.Users
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Matches(@"^[^\d]+$").Length(2, 10);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).NotEmpty().Matches(@"^[^\d]+$");
            RuleFor(x => x.LastName).NotEmpty().Matches(@"^[^\d]+$");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Weight).GreaterThanOrEqualTo(40).LessThanOrEqualTo(150);
            RuleFor(x => x.Height).GreaterThanOrEqualTo(132).LessThanOrEqualTo(228);
        }
    }
}
