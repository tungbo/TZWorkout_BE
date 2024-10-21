namespace TunzWorkout.Api.Models.Dtos.Equipments
{
    public class EquipmentsResponse
    {
        public IEnumerable<EquipmentResponse> Items { get; set; } = new List<EquipmentResponse>();
    }
}
