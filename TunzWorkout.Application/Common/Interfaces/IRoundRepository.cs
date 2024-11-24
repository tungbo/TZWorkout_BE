using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IRoundRepository
    {
        Task CreateAsync(Round round);
        Task DeleteByIdAsync(Guid id);
        Task DeleteByWorkoutId(Guid workoutId);
        Task<bool> UpdateAsync(Round round);

        Task<IEnumerable<Round>> GetAllByWorkoutIdAsync(Guid workoutId);
        Task<IEnumerable<Round>> GetAllAsync();
        Task<Round?> RoundByIdAsync(Guid id);
    }
}
