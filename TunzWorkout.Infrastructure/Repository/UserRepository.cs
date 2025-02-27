using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            return true;
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Email == email);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            return await Task.FromResult(true);
        }
    }
}
