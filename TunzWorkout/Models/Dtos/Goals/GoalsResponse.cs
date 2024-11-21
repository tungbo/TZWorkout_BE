namespace TunzWorkout.Api.Models.Dtos.Goals
{
    public class GoalsResponse
    {
        public IEnumerable<GoalResponse> Items { get; set; } = new List<GoalResponse>();
    }
}
