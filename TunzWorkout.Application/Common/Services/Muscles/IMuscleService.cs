using ErrorOr;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public interface IMuscleService
    {
        Task<ErrorOr<Muscle>> CreateAsync(Muscle muscle);
        Task<ErrorOr<Muscle>> UpdateAsync(Muscle muscle);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id);

        Task<ErrorOr<Muscle>> MuscleByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Muscle>>> GetAllAsync();
    }
}
