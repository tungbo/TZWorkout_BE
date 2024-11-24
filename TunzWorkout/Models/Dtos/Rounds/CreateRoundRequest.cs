using TunzWorkout.Api.Models.Dtos.RoundExercises;

namespace TunzWorkout.Api.Models.Dtos.Rounds
{
    public class CreateRoundRequest
    {
        public string Name { get; set; }
        public int Set { get; set; }
        public int Rest { get; set; }
        public int Order { get; set; }
        public List<RoundExerciseRequest> RoundExerciseRequests { get; set; } = new List<RoundExerciseRequest>();
    }
}
