using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LevelRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Level level)
        {
            await _dbContext.Levels.AddAsync(level);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var level = await _dbContext.Levels.FindAsync(id);
            _dbContext.Levels.Remove(level);
            return true;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Levels.AsNoTracking().AnyAsync(level => level.Id == id);
        }

        public async Task<bool> ExistByIdName(string name)
        {
            return await _dbContext.Levels.AsNoTracking().AnyAsync(level => level.Name == name);
        }

        public async Task<IEnumerable<Level>?> GetAllAsync()
        {
            return await _dbContext.Levels.AsNoTracking().ToListAsync();
        }

        public async Task<Level?> LevelByIdAsync(Guid id)
        {
            return await _dbContext.Levels.AsNoTracking().FirstOrDefaultAsync(level => level.Id == id);
        }

        public async Task<Level?> LevelByNameAsync(string name)
        {
            return await _dbContext.Levels.AsNoTracking().FirstOrDefaultAsync(level => level.Name == name);
        }

        public Task<bool> UpdateAsync(Level level)
        {
            _dbContext.Levels.Update(level);
            return Task.FromResult(true);
        }
    }
}
