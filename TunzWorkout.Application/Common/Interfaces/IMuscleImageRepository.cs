using TunzWorkout.Domain.Entities.MuscleImages;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IMuscleImageRepository
    {
        Task CreateAsync(MuscleImage muscleImage);
        Task DeleteByIdAsync(Guid id);
        Task<bool> UpdateAsync(MuscleImage muscleImage);

        Task<MuscleImage?> GetMuscleImageByMuscleIdAsync(Guid muscleId);
        Task<MuscleImage?> GetMuscleImageByIdAsync(Guid id);
        Task<List<MuscleImage>> GetAllAsync();
    }
}
