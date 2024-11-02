namespace TunzWorkout.Api.Models.Dtos.Exercises
{
    public class ExercisesResponse
    {
        public IEnumerable<ExerciseResponse> Items { get; set; } = new List<ExerciseResponse>();
    }
}
