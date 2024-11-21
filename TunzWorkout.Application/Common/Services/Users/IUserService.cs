using ErrorOr;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Domain.Entities.Users;

namespace TunzWorkout.Application.Common.Services.Users
{
    public interface IUserService
    {
        Task<ErrorOr<AuthenticationCommand>> CreateAsync(RegisterCommand registerCommand);
        Task<ErrorOr<User>> UpdateAsync(User user);
        Task<ErrorOr<User?>> GetByEmailAsync(string email);
        Task<ErrorOr<User?>> GetByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<User>>> GetAllAsync();
    }
}
