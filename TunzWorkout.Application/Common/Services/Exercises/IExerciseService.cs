using ErrorOr;
using TunzWorkout.Application.Common.Filters;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Application.Common.Services.Exercises
{
    public interface IExerciseService
    {
        Task<ErrorOr<Exercise>> CreateAsync(Exercise exercise);
        Task<ErrorOr<Exercise>> UpdateAsync(Exercise exercise);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id);

        Task<int> CountAsync(string? name);
        Task<ErrorOr<Exercise>> ExerciseByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Exercise>>> GetAllAsync(GetAllExercisesOptions options);
    }
}
