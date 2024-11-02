using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.MuscleImages;

namespace TunzWorkout.Domain.Entities.Muscles
{
    public class Muscle
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
        public ICollection<MuscleImage> MuscleImages { get; set; }
    }
}
