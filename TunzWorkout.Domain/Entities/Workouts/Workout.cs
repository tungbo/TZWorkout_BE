using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using TunzWorkout.Domain.Entities.Goals;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Domain.Entities.Wishlists;

namespace TunzWorkout.Domain.Entities.Workouts
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid LevelId { get; set; }
        public Guid GoalId { get; set; }

        public Level Level { get; set; }
        public Goal Goal { get; set; }


        public ICollection<Round> Rounds { get; set; } = new List<Round>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();



    }
}
