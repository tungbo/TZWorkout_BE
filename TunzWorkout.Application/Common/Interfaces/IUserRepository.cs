
using TunzWorkout.Domain.Entities.Users;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> ExistByEmailAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();


    }
}
