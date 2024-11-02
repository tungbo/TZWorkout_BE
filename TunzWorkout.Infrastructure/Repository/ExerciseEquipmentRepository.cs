using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class ExerciseEquipmentRepository : IExerciseEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ExerciseEquipmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddRangeAsync(List<ExerciseEquipment> exerciseEquipments)
        {
            await _dbContext.ExerciseEquipments.AddRangeAsync(exerciseEquipments);
        }

        public Task DeleteRangeAsync(List<ExerciseEquipment> exerciseEquipments)
        {
            _dbContext.ExerciseEquipments.RemoveRange(exerciseEquipments);
            return Task.CompletedTask;
        }

        public async Task<List<ExerciseEquipment>> GetByExerciseIdAsync(Guid exerciseId)
        {
            return await _dbContext.ExerciseEquipments.AsNoTracking().Where(x => x.ExerciseId == exerciseId).ToListAsync();
        }
    }
}
