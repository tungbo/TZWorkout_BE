using ErrorOr;
using TunzWorkout.Domain.Entities.Wishlists;

namespace TunzWorkout.Application.Common.Services.Wishlists
{
    public interface IWishlistService
    {
        Task<ErrorOr<Wishlist>> CreateAsync(Wishlist wishlist);
        Task<ErrorOr<Wishlist>> UpdateAsync(Wishlist wishlist);
        Task<ErrorOr<Deleted>> DeleteAsync(Guid id);

        Task<ErrorOr<Wishlist?>> GetByIdAsync(Guid id);
        Task<ErrorOr<Wishlist?>> GetWishlistByUserIdAndWorkoutId(Guid userId, Guid workoutId);
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllAsync();
        Task<ErrorOr<IEnumerable<Wishlist>>> GetAllByUserIdAsync(Guid userId);
    }
}
