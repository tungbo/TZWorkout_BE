using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<bool> CreateAsync(Equipment equipment);
        Task<bool> UpdateAsync(Equipment equipment);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistByIdAsync(Guid id);

        Task<Equipment> EquipmentByIdAsync(Guid id);
        Task<IEnumerable<Equipment>> GetAllAsync();
    }
}
