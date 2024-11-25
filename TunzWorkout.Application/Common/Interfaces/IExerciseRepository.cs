using TunzWorkout.Application.Common.Filters;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IExerciseRepository
    {
        Task<bool> CreateAsync(Exercise exercise);
        Task<bool> UpdateAsync(Exercise exercise);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistByIdAsync(Guid id);

        Task<int> CountAsync(string? name);
        Task<Exercise?> ExerciseByIdAsync(Guid id);
        Task<IEnumerable<Exercise>> GetAllAsync(GetAllExercisesOptions options);
    }
}
