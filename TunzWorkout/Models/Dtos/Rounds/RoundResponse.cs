using TunzWorkout.Api.Models.Dtos.RoundExercises;

namespace TunzWorkout.Api.Models.Dtos.Rounds
{
    public class RoundResponse
    {
        public string Name { get; set; }
        public int Set { get; set; }
        public int Rest { get; set; }
        public int Order { get; set; }
        public List<RoundExerciseResponse> RoundExerciseResponses { get; set; } = new List<RoundExerciseResponse>();
    }
}
