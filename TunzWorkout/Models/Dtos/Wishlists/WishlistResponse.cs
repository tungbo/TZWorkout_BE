namespace TunzWorkout.Api.Models.Dtos.Wishlists
{
    public class WishlistResponse
    {
        public Guid Id { get; set; }
        public string WorkoutName { get; set; }
        public string GoalName { get; set; }
        public string LevelName { get; set; }
        public Guid WorkoutId { get; set; }
    }
}
