using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    class EquipmentRepository : IEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EquipmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Equipment equipment)
        {
            await _dbContext.Equipments.AddAsync(equipment);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var equipment = await _dbContext.Equipments.FindAsync(id);
            _dbContext.Equipments.Remove(equipment);
            return true;
        }

        public Task<Equipment?> EquipmentByIdAsync(Guid id)
        {
            return _dbContext.Equipments.AsNoTracking().Include(x=>x.EquipmentImages).FirstOrDefaultAsync(equipment => equipment.Id == id);
        }

        public Task<Equipment?> EquipmentByNameAsync(string name)
        {
            return _dbContext.Equipments.AsNoTracking().FirstOrDefaultAsync(equipment => equipment.Name == name);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Equipments.AnyAsync(equipment => equipment.Id == id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _dbContext.Equipments.AnyAsync(equipment => equipment.Name == name);
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _dbContext.Equipments.AsNoTracking().Include(x => x.EquipmentImages).ToListAsync();
        }

        public Task<bool> UpdateAsync(Equipment equipment)
        {
            _dbContext.Equipments.Update(equipment);
            return Task.FromResult(true);
        }
    }
}
