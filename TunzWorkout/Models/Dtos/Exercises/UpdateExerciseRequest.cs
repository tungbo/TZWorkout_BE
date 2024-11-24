namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class UpdateExerciseRequest
    {
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; }
        public IFormFile? VideoFile { get; set; }

        public List<Guid> SelectedMuscleIds { get; set; }
        public List<Guid> SelectedEquipmentIds { get; set; } = new List<Guid>();
    }
}
