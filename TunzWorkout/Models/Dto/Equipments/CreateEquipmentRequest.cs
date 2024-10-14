namespace TunzWorkout.Api.Models.Dto.Equipments
{
    public class CreateEquipmentRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
