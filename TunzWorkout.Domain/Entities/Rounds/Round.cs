using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Domain.Entities.Rounds
{
    public class Round
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Set { get; set; }
        public int Rest { get; set; }
        public int Order { get; set; }
        public Guid WorkoutId { get; set; }

        public ICollection<RoundExercise> RoundExercises { get; set; } = new List<RoundExercise>();
        public Workout Workout { get; set; }

    }
}
