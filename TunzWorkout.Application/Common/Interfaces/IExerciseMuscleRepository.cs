using TunzWorkout.Domain.Entities.ExerciseMuscles;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IExerciseMuscleRepository
    {
        Task DeleteRangeAsync(List<ExerciseMuscle> exerciseMuscles);
        Task AddRangeAsync(List<ExerciseMuscle> exerciseMuscles);
        Task<List<ExerciseMuscle>> GetByExerciseIdAsync(Guid exerciseId);
    }
}
