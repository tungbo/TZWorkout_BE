using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Images;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Image image)
        {
            await _dbContext.Images.AddAsync(image);
            return true;
        }

        public async Task<Image> ImageByImageableIdAsync(Guid imageableId)
        {
            return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(image => image.ImageableId == imageableId);
        }

        public async Task<Image> ImageIdAsync(Guid? id)
        {
            return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(image => image.Id == id);
        }

        public async Task<bool> UpdateAsync(Image image)
        {
            _dbContext.Images.Update(image);
            return true;
        }
    }
}
