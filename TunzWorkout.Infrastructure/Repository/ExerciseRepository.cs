using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExerciseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Exercise exercise)
        {
            await _dbContext.Exercises.AddAsync(exercise);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var exercise = await _dbContext.Exercises.FindAsync(id);
            if (exercise is not null)
            {
                _dbContext.Exercises.Remove(exercise);
                return true;
            }
            return false;
        }

        public async Task<Exercise> ExerciseByIdAsync(Guid id)
        {
            return await _dbContext.Exercises.AsNoTracking().FirstOrDefaultAsync(exercise => exercise.Id == id);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Exercises.AsNoTracking().AnyAsync(exercise => exercise.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _dbContext.Exercises.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            return true;
        }
    }
}
