namespace TunzWorkout.Api.Models.Dtos.RoundExercises
{
    public class UpdateRoundExerciseRequest
    {
        public Guid Id { get; set; }
        public Guid RoundId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public Guid ExerciseId { get; set; }
        public int Order { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }
    }
}
