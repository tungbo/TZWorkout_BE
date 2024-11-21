using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Api.Models.Dtos.FitnessProfiles
{
    public class UpdateFitnessProfileRequest
    {
        public Guid UserId { get; set; }
        public Guid LevelId { get; set; }
        public Gender Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public ICollection<Guid> SelectedGoalIds { get; set; } = new List<Guid>();
    }
}
