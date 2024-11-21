using FluentValidation;
using TunzWorkout.Domain.Entities.Users;

namespace TunzWorkout.Application.Common.Validators.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().Matches(@"^[^\d]+$");
            RuleFor(x => x.LastName).NotEmpty().Matches(@"^[^\d]+$");
            RuleFor(x => x.Username).NotEmpty().Matches(@"^[^\d]+$").Length(2, 10);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.CreateAt).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEmpty();
        }
    }
}
