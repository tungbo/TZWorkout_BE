using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.UserGoals;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Domain.Entities.FitnessProfiles
{
    public class FitnessProfile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LevelId { get; set; }
        public Gender Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        [ValidateNever]
        [NotMapped]
        public ICollection<Guid> SelectedGoalIds { get; set; } = new List<Guid>();

        public User User { get; set; }
        public Level Level { get; set; }
        public ICollection<UserGoal> UserGoals { get; set; } = new List<UserGoal>();
    }
}
