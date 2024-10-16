
namespace TunzWorkout.Api.Models.Dtos.Equipments
{
    public class EquipmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
