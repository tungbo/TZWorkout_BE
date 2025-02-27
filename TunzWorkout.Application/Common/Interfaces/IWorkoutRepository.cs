using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IWorkoutRepository
    {
        Task CreateAsync(Workout workout);
        Task DeleteByIdAsync(Guid id);
        Task<bool> UpdateAsync(Workout workout);

        Task<bool> ExistByIdAsync(Guid id);
        Task<IEnumerable<Workout>> GetAllAsync();
        Task<Workout?> WorkoutByIdAsync(Guid id);
    }
}
