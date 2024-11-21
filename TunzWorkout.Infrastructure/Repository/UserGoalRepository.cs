using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.UserGoals;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class UserGoalRepository : IUserGoalRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserGoalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddRangeAsync(List<UserGoal> userGoals)
        {
            await _dbContext.UserGoals.AddRangeAsync(userGoals);
        }

        public Task DeleteRangeAsync(List<UserGoal> userGoals)
        {
             _dbContext.UserGoals.RemoveRange(userGoals);
            return Task.CompletedTask;
        }

        public async Task<List<UserGoal>> GetByFitnessProfileId(Guid fitnessProfileId)
        {
            return await _dbContext.UserGoals.AsNoTracking().Where(x => x.FitnessProfileId == fitnessProfileId).ToListAsync();
        }
    }
}
