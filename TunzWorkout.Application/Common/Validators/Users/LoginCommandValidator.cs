using FluentValidation;
using TunzWorkout.Application.Commands.Authentication;

namespace TunzWorkout.Application.Common.Validators.Users
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
