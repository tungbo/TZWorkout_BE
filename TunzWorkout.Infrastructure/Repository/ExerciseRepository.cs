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
            //return await _dbContext.Exercises
            //    .Include(x => x.Level)
            //    .Include(x => x.Videos)
            //    .Include(x => x.ExerciseMuscles).ThenInclude(el => el.Muscle).ThenInclude(el => el.MuscleImages)
            //    .Include(x => x.ExerciseEquipments).ThenInclude(el => el.Equipment).ThenInclude(el => el.EquipmentImages)
            //    .AsNoTracking().FirstOrDefaultAsync(exercise => exercise.Id == id);

            var exercise = await _dbContext.Exercises.AsNoTracking().FirstOrDefaultAsync(exercise => exercise.Id == id);
            var level = await _dbContext.Levels.AsNoTracking().FirstOrDefaultAsync(level => level.Id == exercise.LevelId);
            var videos = await _dbContext.Videos.AsNoTracking().Where(video => video.ExerciseId == exercise.Id).ToListAsync();
            var exerciseMuscles = await _dbContext.ExerciseMuscles
                .Where(em => em.ExerciseId == exercise.Id)
                .Include(em => em.Muscle)
                .ThenInclude(m => m.MuscleImages)
                .AsNoTracking()
                .ToListAsync();
            var exerciseEquipments = await _dbContext.ExerciseEquipments.Where(ee => ee.ExerciseId == exercise.Id)
                .Include(ee => ee.Equipment)
                .ThenInclude(e => e.EquipmentImages)
                .AsNoTracking()
                .ToListAsync();
            exercise.Level = level;
            exercise.Videos = videos;
            exercise.ExerciseMuscles = exerciseMuscles;
            exercise.ExerciseEquipments = exerciseEquipments;
            return exercise;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Exercises.AsNoTracking().AnyAsync(exercise => exercise.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            //return await _dbContext.Exercises
            //    .Include(x => x.Level)
            //    .Include(x => x.Videos)
            //    .Include(x => x.ExerciseMuscles).ThenInclude(el => el.Muscle).ThenInclude(el => el.MuscleImages)
            //    .Include(x => x.ExerciseEquipments).ThenInclude(el => el.Equipment).ThenInclude(el => el.EquipmentImages)
            //    .AsNoTracking().ToListAsync();
            var exercises = await _dbContext.Exercises.AsNoTracking().ToListAsync();

            var exerciseIds = exercises.Select(e => e.Id).ToList();
            var exerciseLevels = exercises.Select(e => e.LevelId).ToList();

            var levels = await _dbContext.Levels
                .Where(l => exerciseLevels.Contains(l.Id))
                .AsNoTracking()
                .ToListAsync();

            var videos = await _dbContext.Videos
                .Where(v => exerciseIds.Contains(v.ExerciseId))
                .AsNoTracking()
                .ToListAsync();

            var exerciseMuscles = await _dbContext.ExerciseMuscles
                .Where(em => exerciseIds.Contains(em.ExerciseId))
                .Include(em => em.Muscle)
                .ThenInclude(m => m.MuscleImages)
                .AsNoTracking()
                .ToListAsync();

            var exerciseEquipments = await _dbContext.ExerciseEquipments
                .Where(ee => exerciseIds.Contains(ee.ExerciseId))
                .Include(ee => ee.Equipment)
                .ThenInclude(e => e.EquipmentImages)
                .AsNoTracking()
                .ToListAsync();

            // Map the related data back to the exercises
            foreach (var exercise in exercises)
            {
                exercise.Level = levels.FirstOrDefault(l => l.Id == exercise.LevelId);
                exercise.Videos = videos.Where(v => v.ExerciseId == exercise.Id).ToList();
                exercise.ExerciseMuscles = exerciseMuscles.Where(em => em.ExerciseId == exercise.Id).ToList();
                exercise.ExerciseEquipments = exerciseEquipments.Where(ee => ee.ExerciseId == exercise.Id).ToList();
            }

            return exercises;
        }

        public Task<bool> UpdateAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            return Task.FromResult(true);
        }
    }
}
