using TunzWorkout.Domain.Entities.RoundExercises;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IRoundExerciseRepository
    {
        Task CreateAsync(RoundExercise roundExercise);
        Task DeleteByIdAsync(Guid id);
        Task DeleteAllByRoundId(Guid roundId);
        Task<bool> UpdateAsync(RoundExercise roundExercise);

        Task<IEnumerable<RoundExercise>> GetAllByRoundId(Guid roundId);
        Task<IEnumerable<RoundExercise>> GetAllAsync();
        Task<RoundExercise?> RoundExerciseByIdAsync(Guid id);
    }
}
