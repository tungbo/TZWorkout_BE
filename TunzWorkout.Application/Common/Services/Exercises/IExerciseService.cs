using ErrorOr;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Application.Common.Services.Exercises
{
    public interface IExerciseService
    {
        Task<ErrorOr<Exercise>> CreateAsync(Exercise exercise);
        Task<Exercise> UpdateAsync(Exercise exercise);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Exercise> ExerciseByIdAsync(Guid id);
        Task<IEnumerable<Exercise>> GetAllAsync();
    }
}
