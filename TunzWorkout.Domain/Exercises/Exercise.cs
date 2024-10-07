
using TunzWorkout.Domain.Levels;
using TunzWorkout.Domain.Videos;

namespace TunzWorkout.Domain.Exercises
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LevelId { get; set; }
        public bool HasEquipment { get; set; }
        public Guid VideoId { get; set; }

        public Level Level { get; set; }
        public Video Video { get; set; }
    }
}
