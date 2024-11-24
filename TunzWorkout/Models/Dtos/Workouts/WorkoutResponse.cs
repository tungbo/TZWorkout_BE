using TunzWorkout.Api.Models.Dtos.Rounds;

namespace TunzWorkout.Api.Models.Dtos.Workouts
{
    public class WorkoutResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid LevelId { get; set; }
        public Guid GoalId { get; set; }
        public List<RoundResponse> RoundResponses { get; set; } = new List<RoundResponse>();
    }
}
