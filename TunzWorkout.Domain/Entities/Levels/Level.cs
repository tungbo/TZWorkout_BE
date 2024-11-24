using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Domain.Entities.Levels
{
    public class Level
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Exercise> Exercises { get; set; }// Quan hệ với Exercise
        public ICollection<FitnessProfile> FitnessProfiles { get; set; }// Quan hệ với FitnessProfile
        public ICollection<Workout> Workouts { get; set; }

    }
}
