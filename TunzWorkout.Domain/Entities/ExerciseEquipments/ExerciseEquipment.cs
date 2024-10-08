using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Domain.Entities.ExerciseEquipments
{
    public class ExerciseEquipment
    {
        public Guid EquipmentId { get; set; }
        public Guid ExerciseId { get; set; }

        public Equipment Equipment { get; set; }
        public Exercise Exercise { get; set; }
    }
}
