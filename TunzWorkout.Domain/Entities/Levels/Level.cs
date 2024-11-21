using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.FitnessProfiles;

namespace TunzWorkout.Domain.Entities.Levels
{
    public class Level
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Exercise> Exercises { get; set; }// Quan hệ với Exercise
        public ICollection<FitnessProfile> FitnessProfiles { get; set; }// Quan hệ với FitnessProfile

    }
}
