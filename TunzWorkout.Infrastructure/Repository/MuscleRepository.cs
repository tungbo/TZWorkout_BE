using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Muscles;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class MuscleRepository : IMuscleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MuscleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Muscle muscle)
        {
            await _dbContext.Muscles.AddAsync(muscle);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var muscle = await _dbContext.Muscles.FindAsync(id);

            _dbContext.Muscles.Remove(muscle);
            return true;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Muscles.AsNoTracking().AnyAsync(muscle => muscle.Id == id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _dbContext.Muscles.AsNoTracking().AnyAsync(muscle => muscle.Name == name);
        }

        public async Task<IEnumerable<Muscle>> GetAllAsync()
        {
            return await _dbContext.Muscles.AsNoTracking().ToListAsync();
        }

        public async Task<Muscle?> MuscleByIdAsync(Guid id)
        {
            return await _dbContext.Muscles.AsNoTracking().FirstOrDefaultAsync(muscle => muscle.Id == id);
        }

        public async Task<Muscle?> MuscleByNameAsync(string name)
        {
            return await _dbContext.Muscles.AsNoTracking().FirstOrDefaultAsync(muscle => muscle.Name == name);
        }

        public async Task<bool> UpdateAsync(Muscle muscle)
        {
            _dbContext.Muscles.Update(muscle);
            return true;
        }
    }
}
