
using TunzWorkout.Domain.Exercises;

namespace TunzWorkout.Domain.Videos
{
    public class Video
    {
        public Guid Id { get; set; }
        public string VideoPath { get; set; }
        public DateTime UploadDate { get; set; }

        public Exercise Exercise { get; set; }
    }
}
