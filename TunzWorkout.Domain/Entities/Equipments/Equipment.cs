using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using TunzWorkout.Domain.Entities.EquipmentImages;
using TunzWorkout.Domain.Entities.ExerciseEquipments;

namespace TunzWorkout.Domain.Entities.Equipments
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; }
        public ICollection<EquipmentImage> EquipmentImages { get; set; }
    }
}
