using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.EquipmentImages;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class EquipmentImageRepository : IEquipmentImageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EquipmentImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(EquipmentImage equipmentImage)
        {
            await _dbContext.EquipmentImages.AddAsync(equipmentImage);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var euipmentImage = await _dbContext.EquipmentImages.FindAsync(id);
            _dbContext.Remove(euipmentImage);
        }

        public async Task<List<EquipmentImage>> GetAllAsync()
        {
            return await _dbContext.EquipmentImages.AsNoTracking().ToListAsync();
        }

        public async Task<EquipmentImage?> GetEquipmentImageByEquipmentIdAsync(Guid equipmentId)
        {
            return await _dbContext.EquipmentImages.AsNoTracking().FirstOrDefaultAsync(x => x.EquipmentId == equipmentId);
        }

        public async Task<EquipmentImage?> GetEquipmentImageByIdAsync(Guid id)
        {
            return await _dbContext.EquipmentImages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(EquipmentImage equipmentImage)
        {
            _dbContext.EquipmentImages.Update(equipmentImage);
            return Task.CompletedTask;
        }
    }
}
