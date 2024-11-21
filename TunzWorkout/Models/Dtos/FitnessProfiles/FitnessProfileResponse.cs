using TunzWorkout.Api.Models.Dtos.Goals;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Api.Models.Dtos.FitnessProfiles
{
    public class FitnessProfileResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LevelId { get; set; }
        public Gender Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public ICollection<GoalResponse> SelectedGoalIds { get; set; } = new List<GoalResponse>();
    }
}
