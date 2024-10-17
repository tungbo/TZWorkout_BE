namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class ExerciseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; }

        public List<Guid> SelectedMuscleIds { get; set; }
        public List<Guid> SelectedEquipmentIds { get; set; }
    }
}
