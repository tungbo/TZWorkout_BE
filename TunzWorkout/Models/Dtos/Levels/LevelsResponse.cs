namespace TunzWorkout.Api.Models.Dtos.Levels
{
    public class LevelsResponse
    {
        public IEnumerable<LevelResponse> Items { get; set; } = new List<LevelResponse>();
    }
}
