using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Application.Common.Interfaces
{
     public interface ILevelRepository
    {
        Task<bool> CreateAsync(Level level);
        Task<bool> UpdateAsync(Level level);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistByIdAsync(Guid id);
        Task<bool> ExistByIdName(string name);

        Task<Level?> LevelByIdAsync(Guid id);
        Task<Level?> LevelByNameAsync(string name);
        Task<IEnumerable<Level>?> GetAllAsync();
    }
}
