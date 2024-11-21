using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IGoalRepository
    {
        Task<bool> CreateAsync(Goal goal);
        Task<bool> UpdateAsync(Goal goal);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistByIdAsync(Guid id);
        Task<bool> ExistByNameAsync(string name);

        Task<Goal?> GoalByIdAsync(Guid id);
        Task<Goal?> GoalByNameAsync(string name);
        Task<IEnumerable<Goal>?> GetAllAsync();
    }
}
