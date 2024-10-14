namespace TunzWorkout.Api.Models.Dto.Muscles
{
    public class UpdateMuscleRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
