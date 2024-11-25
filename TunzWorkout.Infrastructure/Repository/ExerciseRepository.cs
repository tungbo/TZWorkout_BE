using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Filters;
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

        public async Task<IEnumerable<Exercise>> GetAllAsync(GetAllExercisesOptions options)
        {
            //return await _dbContext.Exercises
            //    .Include(x => x.Level)
            //    .Include(x => x.Videos)
            //    .Include(x => x.ExerciseMuscles).ThenInclude(el => el.Muscle).ThenInclude(el => el.MuscleImages)
            //    .Include(x => x.ExerciseEquipments).ThenInclude(el => el.Equipment).ThenInclude(el => el.EquipmentImages)
            //    .AsNoTracking().ToListAsync();

            var query = _dbContext.Exercises.AsNoTracking().AsQueryable();

            if(!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(e => e.Name.Contains(options.Name));
            }
            if(options.LevelId.HasValue)
            {
                query = query.Where(e => e.LevelId == options.LevelId);
            }
            if (options.MuscleId.HasValue)
            {
                query = query.Where(e => e.ExerciseMuscles.Any(em => em.MuscleId == options.MuscleId));
            }
            if (options.EquipmentId.HasValue)
            {
                query = query.Where(e => e.ExerciseEquipments.Any(ee => ee.EquipmentId == options.EquipmentId));
            }
            if (!string.IsNullOrWhiteSpace(options.SortField))
            {
                query = options.SortOrder switch
                {
                    SortOrder.Ascending => query.OrderBy(e => e.Name),
                    SortOrder.Descending => query.OrderByDescending(e => e.Name),
                    _ => query
                };
            }

            query = query.Skip((options.Page - 1) * options.PageSize).Take(options.PageSize);

            var exercises = await query.ToListAsync();

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
        public async Task<int> CountAsync(string? name)
        {
            var query = _dbContext.Exercises.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }
            var totalCount = await query.CountAsync();
            return totalCount;
        }

        public Task<bool> UpdateAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            return Task.FromResult(true);
        }
    }
}
