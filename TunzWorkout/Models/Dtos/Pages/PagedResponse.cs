using TunzWorkout.Api.Models.Dtos.Exercises;

namespace TunzWorkout.Api.Models.Dtos.Pages
{
    public class PagedResponse<TResponse>
    {

        public IEnumerable<TResponse> Items { get; set; } = new List<TResponse>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public bool HasNextPage => Total > (Page * PageSize);
    }
}
