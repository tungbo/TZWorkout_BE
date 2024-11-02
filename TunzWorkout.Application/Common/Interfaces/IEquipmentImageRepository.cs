
using TunzWorkout.Domain.Entities.EquipmentImages;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IEquipmentImageRepository
    {
        Task CreateAsync(EquipmentImage equipmentImage);
        Task UpdateAsync(EquipmentImage equipmentImage);
        Task DeleteByIdAsync(Guid id);

        Task<EquipmentImage?> GetEquipmentImageByEquipmentIdAsync(Guid equipmentId);
        Task<EquipmentImage?> GetEquipmentImageByIdAsync(Guid id);
        Task<List<EquipmentImage>> GetAllAsync();
    }
}
