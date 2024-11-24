using ErrorOr;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Application.Common.Services.Workouts
{
    public interface IWorkoutService
    {
        Task<ErrorOr<Workout>> CreateAsync(Workout workout);
        Task<ErrorOr<Workout>> UpdateAsync(Workout workout);
        Task<ErrorOr<Deleted>> DeleteAsync(Guid id);
        Task<ErrorOr<Workout?>> GetByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Workout>>> GetAllAsync();
    }
}
