using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Domain.Entities.Videos
{
    public class Video
    {
        public Guid Id { get; set; }
        public string VideoPath { get; set; }
        public DateTime UploadDate { get; set; }

        public Exercise Exercise { get; set; }
    }
}
