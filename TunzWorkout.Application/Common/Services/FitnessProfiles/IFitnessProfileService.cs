using ErrorOr;
using TunzWorkout.Domain.Entities.FitnessProfiles;

namespace TunzWorkout.Application.Common.Services.FitnessProfiles
{
    public interface IFitnessProfileService
    {
        Task<ErrorOr<FitnessProfile>> CreateAsync(FitnessProfile fitnessProfile);
        Task<ErrorOr<FitnessProfile>> UpdateAsync(FitnessProfile fitnessProfile);
        Task<ErrorOr<FitnessProfile>> GetFitnessProfileByIdAsync(Guid id);
        Task<ErrorOr<FitnessProfile>> GetFitnessProfileByUserIdAsync(Guid userId);
        Task<ErrorOr<IEnumerable<FitnessProfile>>> GetAllAsync();
    }
}
