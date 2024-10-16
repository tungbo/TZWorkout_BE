namespace TunzWorkout.Api.Models.Dtos.Equipments
{
    public class CreateEquipmentRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
