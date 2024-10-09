using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public interface IMuscleService
    {
        Task<bool> CreateAsync(Muscle muscle);
        Task<Muscle> UpdateAsync(Muscle muscle);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Muscle> MuscleByIdAsync(Guid id);
        Task<IEnumerable<Muscle>> GetAllAsync();
    }
}
