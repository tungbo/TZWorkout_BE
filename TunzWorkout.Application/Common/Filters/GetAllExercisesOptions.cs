
namespace TunzWorkout.Application.Common.Filters
{
    public class GetAllExercisesOptions
    {
        public string? Name { get; set; }
        public Guid? LevelId { get; set; }
        public Guid? MuscleId { get; set; }
        public Guid? EquipmentId { get; set; }

        public string? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public enum SortOrder
    {
        Unsorted,
        Ascending,
        Descending
    }
}
