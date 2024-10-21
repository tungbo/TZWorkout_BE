using ErrorOr;
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Services.Equipments
{
    public interface IEquipmentService
    {
        Task<ErrorOr<Equipment>> CreateAsync(Equipment equipment);
        Task<ErrorOr<Equipment>> UpdateAsync(Equipment equipment);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id);

        Task<ErrorOr<Equipment>> EquipmentByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Equipment>>> GetAllAsync();
    }
}
