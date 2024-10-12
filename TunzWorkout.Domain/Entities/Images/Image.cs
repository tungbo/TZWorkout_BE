namespace TunzWorkout.Domain.Entities.Images
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string Type { get; set; }
        public Guid ImageableId { get; set; }

    }
}
