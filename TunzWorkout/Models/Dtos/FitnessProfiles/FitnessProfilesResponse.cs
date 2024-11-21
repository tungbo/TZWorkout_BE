namespace TunzWorkout.Api.Models.Dtos.FitnessProfiles
{
    public class FitnessProfilesResponse
    {
        public IEnumerable<FitnessProfileResponse> Items { get; set; } = new List<FitnessProfileResponse>();
    }
}
