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

        public async Task<Exercise?> ExerciseByIdAsync(Guid id)
        {
            return await _dbContext.Exercises.AsNoTracking()
                .Include(x => x.Level)
                .Include(x => x.ExerciseMuscles).ThenInclude(el => el.Muscle).ThenInclude(el => el.MuscleImages)
                .Include(x => x.ExerciseEquipments).ThenInclude(el => el.Equipment).ThenInclude(el => el.EquipmentImages)
                .FirstOrDefaultAsync(exercise => exercise.Id == id);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Exercises.AsNoTracking().AnyAsync(exercise => exercise.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _dbContext.Exercises.AsNoTracking()
                .Include(x => x.Level)
                .Include(x => x.ExerciseMuscles).ThenInclude(el => el.Muscle).ThenInclude(el => el.MuscleImages)
                .Include(x => x.ExerciseEquipments).ThenInclude(el => el.Equipment).ThenInclude(el => el.EquipmentImages)
                .ToListAsync();
        }

        public Task<bool> UpdateAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            return Task.FromResult(true);
        }
    }
}
