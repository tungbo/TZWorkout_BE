
namespace TunzWorkout.Domain.Equipments
{
    public class Equipment
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid ImageId { get; set; }
    }
}
