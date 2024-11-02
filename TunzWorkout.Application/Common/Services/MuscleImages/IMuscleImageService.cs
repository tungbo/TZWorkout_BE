using TunzWorkout.Domain.Entities.MuscleImages;

namespace TunzWorkout.Application.Common.Services.MuscleImages
{
    public interface IMuscleImageService
    {
        Task CreateAsync(MuscleImage muscleImage);
        Task DeleteByIdAsync(Guid id);
    }
}
