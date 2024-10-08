using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.Images;

namespace TunzWorkout.Domain.Entities.Equipments
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ImageId { get; set; }

        public Image Image { get; set; }
        public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; }

    }
}
