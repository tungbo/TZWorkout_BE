using TunzWorkout.Api.Models.Dtos.Pages;

namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class GetAllExercisesRequest : PagedRequest
    {
        public string? Name { get; set; }
        public Guid? LevelId { get; set; }
        public Guid? MuscleId { get; set; }
        public Guid? EquipmentId { get; set; }
        public string? SortBy { get; set; }
    }
}
