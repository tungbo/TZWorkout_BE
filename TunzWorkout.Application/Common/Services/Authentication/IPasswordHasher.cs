using ErrorOr;

namespace TunzWorkout.Application.Common.Services.Authentication
{
    public interface IPasswordHasher
    {
        public string HashPassword(string password);
        bool IsCorrectPassword(string password, string hashedPassword);
    }
}
