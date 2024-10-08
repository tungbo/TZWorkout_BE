namespace TunzWorkout.Api.Models.Dto.Muscles
{
    public class MuscleUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ImageId { get; set; }
    }
}
