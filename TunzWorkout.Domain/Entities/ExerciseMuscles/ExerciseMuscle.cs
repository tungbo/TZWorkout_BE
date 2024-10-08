using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Domain.Entities.ExerciseMuscles
{
    public class ExerciseMuscle
    {
        public Guid MuscleId { get; set; }
        public Guid ExerciseId { get; set; }

        public Muscle Muscle { get; set; }
        public Exercise Exercise { get; set; }
    }
}
