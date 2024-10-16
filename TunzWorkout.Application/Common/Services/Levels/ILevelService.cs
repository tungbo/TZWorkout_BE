using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Application.Common.Services.Levels
{
    public interface ILevelService
    {
        Task<bool> CreateAsync(Level level);
        Task<Level> UpdateAsync(Level level);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<Level> LevelByIdAsync(Guid id);
        Task<IEnumerable<Level>> GetAllAsync();
    }
}
