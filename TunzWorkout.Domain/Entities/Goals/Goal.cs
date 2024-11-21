using TunzWorkout.Domain.Entities.UserGoals;

namespace TunzWorkout.Domain.Entities.Goals
{
    public class Goal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserGoal> UserGoals { get; set; }
    }
}
