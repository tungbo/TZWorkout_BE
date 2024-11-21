using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    class FitnessProfileRepository : IFitnessProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FitnessProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(FitnessProfile fitnessProfile)
        {
            await _dbContext.FitnessProfiles.AddAsync(fitnessProfile);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var fitnessProfile = await _dbContext.FitnessProfiles.FindAsync(id);
            _dbContext.FitnessProfiles.Remove(fitnessProfile);
            return true;
        }

        public async Task<FitnessProfile?> FitnessProfileById(Guid id)
        {
            return await _dbContext.FitnessProfiles.Include(x => x.UserGoals).ThenInclude(x => x.Goal).FirstOrDefaultAsync(fp => fp.Id == id);
        }

        public async Task<FitnessProfile?> FitnessProfileByUserId(Guid userId)
        {
            return await _dbContext.FitnessProfiles.AsNoTracking().Include(x => x.UserGoals).ThenInclude(x => x.Goal).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<FitnessProfile>?> GetAllAsync()
        {
            return await _dbContext.FitnessProfiles.AsNoTracking().Include(x => x.UserGoals).ThenInclude(x => x.Goal).ToListAsync();
        }

        public async Task<bool> UpdateAsync(FitnessProfile fitnessProfile)
        {
            _dbContext.FitnessProfiles.Update(fitnessProfile);
            return await Task.FromResult(true);
        }
    }
}
