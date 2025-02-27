using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Workouts;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WorkoutRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Workout workout)
        {
            await _dbContext.Workouts.AddAsync(workout);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _dbContext.Workouts.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Workouts.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            return await _dbContext.Workouts.AsNoTracking().Include(x => x.Goal).Include(x => x.Level).Include(x => x.Rounds).ThenInclude(x => x.RoundExercises).ThenInclude(x => x.Exercise).ThenInclude(x => x.Videos).ToListAsync();
        }


        public async Task<bool> UpdateAsync(Workout workout)
        {
            _dbContext.Workouts.Update(workout);
            return await Task.FromResult(true);
        }

        public async Task<Workout?> WorkoutByIdAsync(Guid id)
        {
            return await _dbContext.Workouts.AsNoTracking().Include(x => x.Goal).Include(x => x.Level).Include(x => x.Rounds).ThenInclude(x => x.RoundExercises).ThenInclude(x => x.Exercise).ThenInclude(x => x.Videos).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
