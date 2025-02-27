namespace TunzWorkout.Api.Models.Dtos.Wishlists
{
    public class CreateWishlistRequest
    {
        public Guid UserId { get; set; }
        public Guid WorkoutId { get; set; }
    }
}
