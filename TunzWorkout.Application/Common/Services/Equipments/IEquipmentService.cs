using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Services.Equipments
{
    public interface IEquipmentService
    {
        Task<bool> CreateAsync(Equipment equipment);
        Task<Equipment> UpdateAsync(Equipment equipment);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Equipment> EquipmentByIdAsync(Guid id);
        Task<IEnumerable<Equipment>> GetAllAsync();
    }
}
