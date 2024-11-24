namespace TunzWorkout.Api.Models.Dtos.RoundExercises
{
    public class RoundExerciseResponse
    {
        public Guid RoundId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Order { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }
    }
}
