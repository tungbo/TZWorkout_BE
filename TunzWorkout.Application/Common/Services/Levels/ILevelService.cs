using ErrorOr;
using TunzWorkout.Domain.Entities.Levels;
namespace TunzWorkout.Application.Common.Services.Levels
{
    public interface ILevelService
    {
        Task<ErrorOr<Level>> CreateAsync(Level level);
        Task<ErrorOr<Level>> UpdateAsync(Level level);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id);

        Task<ErrorOr<Level>> GetLevelByIdAsync(Guid id);
        //Task<ErrorOr<Level>> GetLevelByNameAsync(Guid id);
        Task<ErrorOr<IEnumerable<Level>>> GetAllAsync();
    }
}
