using ErrorOr;
using System.Linq;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Wishlists;

namespace TunzWorkout.Application.Common.Services.Wishlists
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUnitOfWork _unitOfWork;
        public WishlistService(IWishlistRepository wishlistRepository, IUserRepository userRepository, IWorkoutRepository workoutRepository, IUnitOfWork unitOfWork)
        {
            _wishlistRepository = wishlistRepository;
            _workoutRepository = workoutRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Wishlist>> CreateAsync(Wishlist wishlist)
        {
            if(!await _userRepository.ExistByIdAsync(wishlist.UserId))
            {
                return Error.NotFound(description: "User not found");
            }
            if(!await _workoutRepository.ExistByIdAsync(wishlist.WorkoutId))
            {
                return Error.NotFound(description: "Workout not found");
            }
            var currentWishlist = await _wishlistRepository.GetWishlistIdByUserIdAsync(wishlist.UserId);
            if (currentWishlist.Contains(wishlist.WorkoutId))
            {
                return Error.Conflict(description: "Workout already in wishlist");
            }

            await _wishlistRepository.CreateAsync(wishlist);
            await _unitOfWork.CommitChangesAsync();

            return wishlist;
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id)
        {
            if(!await _wishlistRepository.ExistByIdAsync(id))
            {
                return Error.NotFound(description: "Wishlist not found");
            }
            await _wishlistRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<IEnumerable<Wishlist>>> GetAllAsync()
        {
            var wishlists = await _wishlistRepository.GetAllAsync();
            if (wishlists is null || !wishlists.Any())
            {
                return Error.NotFound(description: "No wishlist found");
            }
            return wishlists.ToList();
        }

        public async Task<ErrorOr<IEnumerable<Wishlist>>> GetAllByUserIdAsync(Guid userId)
        {
            var wishlists = await _wishlistRepository.GetAllByUserIdAsync(userId);
            if(wishlists is null || !wishlists.Any())
            {
                return Error.NotFound(description: "No wishlist found");
            }
            return wishlists.ToList();
        }

        public async Task<ErrorOr<Wishlist?>> GetByIdAsync(Guid id)
        {
            if(!await _wishlistRepository.ExistByIdAsync(id))
            {
                return Error.NotFound(description: "Wishlist not found");
            }
            return await _wishlistRepository.WishlistByIdAsync(id);
        }

        public async Task<ErrorOr<Wishlist?>> GetWishlistByUserIdAndWorkoutId(Guid userId, Guid workoutId)
        {
            var wishlist = await _wishlistRepository.GetWishlistByUserIdAndWorkoutId(userId, workoutId);
            if (wishlist is null)
            {
                return Error.NotFound(description: "Wishlist not found");
            }
            return wishlist;
        }

        public Task<ErrorOr<Wishlist>> UpdateAsync(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }
    }
}
