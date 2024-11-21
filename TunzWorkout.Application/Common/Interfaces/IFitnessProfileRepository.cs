using TunzWorkout.Domain.Entities.FitnessProfiles;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IFitnessProfileRepository
    {
        Task<bool> CreateAsync(FitnessProfile fitnessProfile);
        Task<bool> UpdateAsync(FitnessProfile fitnessProfile);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<FitnessProfile?> FitnessProfileById(Guid id);
        Task<FitnessProfile?> FitnessProfileByUserId(Guid userId);
        Task<IEnumerable<FitnessProfile>?> GetAllAsync();
    }
}
