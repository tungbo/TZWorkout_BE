using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Wishlists;
using TunzWorkout.Infrastructure.Data;

namespace TunzWorkout.Infrastructure.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WishlistRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(Wishlist wishlist)
        {
            await _dbContext.Wishlists.AddAsync(wishlist);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _dbContext.Wishlists.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await _dbContext.Wishlists.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Wishlist>> GetAllAsync()
        {
            return await _dbContext.Wishlists.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Wishlist>> GetAllByUserIdAsync(Guid userId)
        {
            return await _dbContext.Wishlists.Include(x => x.Workout).ThenInclude(x => x.Goal).Include(x => x.Workout).ThenInclude(x => x.Level).Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<Wishlist?> GetWishlistByUserIdAndWorkoutId(Guid userId, Guid workoutId)
        {
            return await _dbContext.Wishlists.Include(x => x.Workout).ThenInclude(x => x.Goal).Include(x => x.Workout).ThenInclude(x => x.Level).AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId && x.WorkoutId == workoutId);
        }

        public async Task<IEnumerable<Guid>> GetWishlistIdByUserIdAsync(Guid userId)
        {
            return await _dbContext.Wishlists.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.WorkoutId).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Wishlist wishlist)
        {
            _dbContext.Wishlists.Update(wishlist);
            return await Task.FromResult(true);
        }

        public async Task<Wishlist?> WishlistByIdAsync(Guid id)
        {
            return await _dbContext.Wishlists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        
    }
}
