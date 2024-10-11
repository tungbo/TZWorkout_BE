using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Images;

namespace TunzWorkout.Domain.Entities.Muscles
{
    public class Muscle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? MuscleImageId { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<ExerciseMuscle> ExerciseMuscles { get; set; }
    }
}
