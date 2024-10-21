namespace TunzWorkout.Api.Models.Dtos.Muscles
{
    public class MusclesResponse
    {
        public IEnumerable<MuscleResponse> Items { get; set; } = new List<MuscleResponse>();
    }
}
