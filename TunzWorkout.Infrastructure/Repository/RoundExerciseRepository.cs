using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class RoundExerciseRepository : IRoundExerciseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RoundExerciseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(RoundExercise roundExercise)
        {
            await _dbContext.RoundExercises.AddAsync(roundExercise);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _dbContext.RoundExercises.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteAllByRoundId(Guid roundId)
        {
            await _dbContext.RoundExercises.Where(x => x.RoundId == roundId).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<RoundExercise>> GetAllAsync()
        {
            return await _dbContext.RoundExercises.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RoundExercise>> GetAllByRoundId(Guid roundId)
        {
            return await _dbContext.RoundExercises.AsNoTracking().Where(x => x.RoundId == roundId).ToListAsync();
        }

        public async Task<RoundExercise?> RoundExerciseByIdAsync(Guid id)
        {
            return await _dbContext.RoundExercises.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(RoundExercise roundExercise)
        {
            _dbContext.RoundExercises.Update(roundExercise);
            return await Task.FromResult(true);
        }
    }
}
