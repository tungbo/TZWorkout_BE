using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Domain.Entities.Wishlists
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkoutId { get; set; }

        public User User { get; set; }
        public Workout Workout { get; set; }
    }
}
