using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Goals;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GoalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Goal goal)
        {
            await _dbContext.Goals.AddAsync(goal);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var goal = await _dbContext.Goals.FindAsync(id);
            _dbContext.Goals.Remove(goal);
            return true;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Goals.AsNoTracking().AnyAsync(goal => goal.Id == id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _dbContext.Goals.AsNoTracking().AnyAsync(goal => goal.Name == name);
        }

        public async Task<IEnumerable<Goal>?> GetAllAsync()
        {
            return await _dbContext.Goals.AsNoTracking().ToListAsync();
        }

        public async Task<Goal?> GoalByIdAsync(Guid id)
        {
            return await _dbContext.Goals.AsNoTracking().FirstOrDefaultAsync(goal => goal.Id == id);
        }

        public async Task<Goal?> GoalByNameAsync(string name)
        {
            return await _dbContext.Goals.AsNoTracking().FirstOrDefaultAsync(goal => goal.Name == name);
        }

        public async Task<bool> UpdateAsync(Goal goal)
        {
            _dbContext.Goals.Update(goal);
            return await Task.FromResult(true);
        }
    }
}
