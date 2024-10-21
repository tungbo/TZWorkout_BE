namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class ExerciseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; }

        public ICollection<Guid> SelectedMuscleIds { get; set; } = new List<Guid>();
        public ICollection<Guid> SelectedEquipmentIds { get; set; } = new List<Guid>();
    }
}
