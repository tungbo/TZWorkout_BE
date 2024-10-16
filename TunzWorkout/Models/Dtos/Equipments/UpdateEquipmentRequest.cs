namespace TunzWorkout.Api.Models.Dtos.Equipments
{
    public class UpdateEquipmentRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
