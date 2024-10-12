using TunzWorkout.Domain.Entities.Images;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IImageRepository
    {
        Task<bool> CreateAsync(Image image);
        Task<bool> UpdateAsync(Image image);

        Task<Image> ImageIdAsync(Guid? id);
        Task<Image> ImageByImageableIdAsync(Guid imageableId);
    }
}
