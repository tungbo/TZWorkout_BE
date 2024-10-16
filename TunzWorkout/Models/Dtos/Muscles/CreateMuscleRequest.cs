namespace TunzWorkout.Api.Models.Dtos.Muscles
{
    public class CreateMuscleRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
