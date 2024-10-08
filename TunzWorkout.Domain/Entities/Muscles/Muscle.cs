using System.Text.Json.Serialization;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Images;

namespace TunzWorkout.Domain.Entities.Muscles
{
    public class Muscle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ImageId { get; set; }

        public Image Image { get; set; }
        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
    }
}
