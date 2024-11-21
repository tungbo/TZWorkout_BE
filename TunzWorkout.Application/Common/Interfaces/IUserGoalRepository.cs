using TunzWorkout.Domain.Entities.UserGoals;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IUserGoalRepository
    {
        Task DeleteRangeAsync(List<UserGoal> userGoals);
        Task AddRangeAsync(List<UserGoal> userGoals);

        Task<List<UserGoal>> GetByFitnessProfileId(Guid fitnessProfileId);
    }
}
