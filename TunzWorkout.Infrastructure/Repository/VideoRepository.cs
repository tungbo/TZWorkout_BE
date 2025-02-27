using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Videos;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VideoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateAsync(Video video)
        {
            await _dbContext.Videos.AddAsync(video);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            await _dbContext.Videos.Where(x => x.Id == id).ExecuteDeleteAsync();
            return true;
        }

        public async Task<Video?> VideoByExerciseIdAsync(Guid exerciseId)
        {
            return await _dbContext.Videos.FirstOrDefaultAsync(x => x.ExerciseId == exerciseId);
        }

        public async Task<Video?> VideoIdAsync(Guid? id)
        {
            return await _dbContext.Videos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> UpdateAsync(Video video)
        {
            _dbContext.Videos.Update(video);
            return Task.FromResult(true);
        }
    }
}
