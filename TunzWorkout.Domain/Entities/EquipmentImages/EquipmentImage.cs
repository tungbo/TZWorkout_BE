
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Domain.Entities.EquipmentImages
{
    public class EquipmentImage
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}
