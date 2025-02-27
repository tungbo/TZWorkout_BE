using TunzWorkout.Domain.Entities.Wishlists;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IWishlistRepository
    {
        Task CreateAsync(Wishlist wishlist);
        Task DeleteByIdAsync(Guid id);
        Task<bool> UpdateAsync(Wishlist wishlist);

        Task<bool> ExistByIdAsync(Guid id);
        Task<IEnumerable<Wishlist>> GetAllAsync();
        Task<IEnumerable<Wishlist>> GetAllByUserIdAsync(Guid userId);
        Task<Wishlist?> WishlistByIdAsync(Guid id);
        Task<IEnumerable<Guid>> GetWishlistIdByUserIdAsync(Guid userId);
        Task<Wishlist?> GetWishlistByUserIdAndWorkoutId(Guid userId, Guid workoutId);
    }
}
