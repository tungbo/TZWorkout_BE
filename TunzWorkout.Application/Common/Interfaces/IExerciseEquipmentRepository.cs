using TunzWorkout.Domain.Entities.ExerciseEquipments;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IExerciseEquipmentRepository
    {
        Task<List<ExerciseEquipment>> GetByExerciseIdAsync(Guid exerciseId);
        Task DeleteRangeAsync(List<ExerciseEquipment> exerciseEquipments);
        Task AddRangeAsync(List<ExerciseEquipment> exerciseEquipments);

    }
}
