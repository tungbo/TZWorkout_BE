using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Videos;

namespace TunzWorkout.Domain.Entities.Exercises
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; } = false;

        public Level Level { get; set; }

        
        public ICollection<Video> Videos { get; set; }
        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
        public ICollection<ExerciseEquipment> ExerciseEquipments { get; set; }
    }
}
