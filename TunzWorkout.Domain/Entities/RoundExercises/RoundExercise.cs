using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Domain.Entities.RoundExercises
{
    public class RoundExercise
    {
        public Guid Id { get; set; }
        public Guid RoundId { get; set; }
        public Guid ExerciseId { get; set; }
        public int Order { get; set; }
        public int Reps { get; set; }
        public int Rest { get; set; }

        public Round Round { get; set; }
        public Exercise Exercise { get; set; }
    }
}
