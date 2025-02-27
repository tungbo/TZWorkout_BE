namespace TunzWorkout.Api.Models.Dtos.Wishlists
{
    public class WishlistsResponse
    {
        public IEnumerable<WishlistResponse> Wishlists { get; set; } = new List<WishlistResponse>();
    }
}
