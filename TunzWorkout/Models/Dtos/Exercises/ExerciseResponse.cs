using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Api.Models.Dtos.Muscles;

namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class ExerciseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LevelName { get; set; }
        public bool HasEquipment { get; set; }
        public string? VideoUrl { get; set; }

        public ICollection<MuscleResponse> SelectedMuscles { get; set; } = new List<MuscleResponse>();
        public ICollection<EquipmentResponse> SelectedEquipments { get; set; } = new List<EquipmentResponse>();
    }
}
