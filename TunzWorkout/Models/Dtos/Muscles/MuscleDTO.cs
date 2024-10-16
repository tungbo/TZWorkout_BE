namespace TunzWorkout.Api.Models.Dtos.Muscles
{
    public class MuscleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
