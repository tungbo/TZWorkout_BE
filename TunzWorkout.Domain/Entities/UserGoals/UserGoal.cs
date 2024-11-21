using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Domain.Entities.UserGoals
{
    public class UserGoal
    {
        public Guid FitnessProfileId { get; set; }
        public Guid GoalId { get; set; }

        public FitnessProfile FitnessProfile { get; set; }
        public Goal Goal { get; set; }
    }
}
