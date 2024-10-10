using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Domain.Entities.Images
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadDate { get; set; }

        public Muscle Muscle { get; set; }
        public Equipment Equipment { get; set; }

    }
}
