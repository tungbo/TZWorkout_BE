using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Domain.Entities.MuscleImages
{
    public class MuscleImage
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid MuscleId { get; set; }
        public Muscle Muscle { get; set; }
    }
}
