namespace TunzWorkout.Api.Models.Dtos.Muscles
{
    public class UpdateMuscleRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
