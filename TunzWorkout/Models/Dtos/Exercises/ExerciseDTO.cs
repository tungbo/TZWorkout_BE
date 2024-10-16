
namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class ExerciseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; } = false;
    }
}
