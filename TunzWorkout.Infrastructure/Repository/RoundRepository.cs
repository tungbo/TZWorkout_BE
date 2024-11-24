using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class RoundRepository : IRoundRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RoundRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Round round)
        {
            await _dbContext.Rounds.AddAsync(round);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _dbContext.Rounds.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteByWorkoutId(Guid workoutId)
        {
            await _dbContext.Rounds.Where(x => x.WorkoutId == workoutId).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Round>> GetAllAsync()
        {
            return await _dbContext.Rounds.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Round>> GetAllByWorkoutIdAsync(Guid workoutId)
        {
            return await _dbContext.Rounds.AsNoTracking().Where(x => x.WorkoutId == workoutId).ToListAsync();
        }

        public async Task<Round?> RoundByIdAsync(Guid id)
        {
            return await _dbContext.Rounds.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Round round)
        {
            _dbContext.Rounds.Update(round);
            return await Task.FromResult(true);
        }
    }
}
