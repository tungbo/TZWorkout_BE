using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.MuscleImages;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class MuscleImageRepository : IMuscleImageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MuscleImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(MuscleImage muscleImage)
        {
            await _dbContext.MuscleImages.AddAsync(muscleImage);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _dbContext.MuscleImages.Where(m => m.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<MuscleImage>> GetAllAsync()
        {
            return await _dbContext.MuscleImages.AsNoTracking().ToListAsync();
        }

        public async Task<MuscleImage?> GetMuscleImageByIdAsync(Guid id)
        {
            return await _dbContext.MuscleImages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MuscleImage?> GetMuscleImageByMuscleIdAsync(Guid muscleId)
        {
            return await _dbContext.MuscleImages.AsNoTracking().FirstOrDefaultAsync(x => x.MuscleId == muscleId);
        }

        public async Task<bool> UpdateAsync(MuscleImage muscleImage)
        {
            _dbContext.Update(muscleImage);
            return await Task.FromResult(true);
        }
    }
}
