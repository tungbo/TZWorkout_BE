using TunzWorkout.Api.Models.Dtos.Rounds;

namespace TunzWorkout.Api.Models.Dtos.Workouts
{
    public class CreateWorkoutRequest
    {
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public Guid GoalId { get; set; }
        public List<CreateRoundRequest> RoundRequests { get; set; } = new List<CreateRoundRequest>();
    }
}
