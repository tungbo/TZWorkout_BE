using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class ExerciseMuscleRepository : IExerciseMuscleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExerciseMuscleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddRangeAsync(List<ExerciseMuscle> exerciseMuscles)
        {
            await _dbContext.ExerciseMuscles.AddRangeAsync(exerciseMuscles);
        }

        public Task DeleteRangeAsync(List<ExerciseMuscle> exerciseMuscles)
        {
            _dbContext.ExerciseMuscles.RemoveRange(exerciseMuscles);
            return Task.CompletedTask;
        }

        public async Task<List<ExerciseMuscle>> GetByExerciseIdAsync(Guid exerciseId)
        {
            return await _dbContext.ExerciseMuscles.AsNoTracking().Where(x => x.ExerciseId == exerciseId).ToListAsync();
        }
    }
}
