using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Wishlists;
using TunzWorkout.Application.Common.Services.Wishlists;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistService _wishlistService;
        public WishlistsController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWishlistRequest request)
        {
            var toWishlist = request.MapToWishlist();
            var result = await _wishlistService.CreateAsync(toWishlist);
            return result.Match(wishlist => Ok(wishlist.MapToResponse()), Problem);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetAllByUserIdAsync(Guid userId)
        {
            var result = await _wishlistService.GetAllByUserIdAsync(userId);
            return result.Match(wishlists => Ok(wishlists.MapToResponse()), Problem);
        }

        [HttpGet("{userId:guid}/{workoutId:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid userId, Guid workoutId)
        {
            var result = await _wishlistService.GetWishlistByUserIdAndWorkoutId(userId, workoutId);
            return result.Match(wishlist => Ok(wishlist?.MapToResponse()), Problem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _wishlistService.DeleteAsync(id);
            return result.Match(_ => NoContent(), Problem);
        }

    }
}
