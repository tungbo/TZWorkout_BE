using ErrorOr;
using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Application.Common.Services.Goals
{
    public interface IGoalService
    {
        Task<ErrorOr<Goal>> CreateAsync(Goal goal);
        Task<ErrorOr<Goal>> UpdateAsync(Goal goal);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id);

        Task<ErrorOr<Goal>> GetGoalByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Goal>>> GetAllAsync();
    }
}
