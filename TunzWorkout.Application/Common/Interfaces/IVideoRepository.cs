using TunzWorkout.Domain.Entities.Videos;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IVideoRepository
    {
        Task<bool> CreateAsync(Video video);
        Task<bool> UpdateAsync(Video video);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Video?> VideoIdAsync(Guid? id);
        Task<Video?> VideoByExerciseIdAsync(Guid exerciseId);
    }
}
