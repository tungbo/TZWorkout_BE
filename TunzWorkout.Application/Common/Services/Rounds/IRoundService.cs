using ErrorOr;
using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Application.Common.Services.Rounds
{
    public interface IRoundService
    {
        Task<ErrorOr<Round>> CreateAsync(Round round);
        Task<ErrorOr<Round>> UpdateAsync(Round round);
        Task<ErrorOr<Deleted>> DeleteAsync(Guid id);
        Task<ErrorOr<Deleted>> DeleteRoundExAsync(Guid id);
        Task<ErrorOr<Round?>> GetByIdAsync(Guid id);
        Task<ErrorOr<IEnumerable<Round>>> GetAllAsync();
    }
}
