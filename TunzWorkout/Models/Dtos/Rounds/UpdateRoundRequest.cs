using TunzWorkout.Api.Models.Dtos.RoundExercises;

namespace TunzWorkout.Api.Models.Dtos.Rounds
{
    public class UpdateRoundRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Set { get; set; }
        public int Rest { get; set; }
        public int Order { get; set; }
        public List<UpdateRoundExerciseRequest> RoundExerciseRequests { get; set; } = new List<UpdateRoundExerciseRequest>();
    }
}
