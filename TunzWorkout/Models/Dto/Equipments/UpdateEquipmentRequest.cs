namespace TunzWorkout.Api.Models.Dto.Equipments
{
    public class UpdateEquipmentRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
