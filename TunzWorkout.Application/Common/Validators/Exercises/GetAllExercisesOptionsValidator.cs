using FluentValidation;
using TunzWorkout.Application.Common.Filters;

namespace TunzWorkout.Application.Common.Validators.Exercises
{
    class GetAllExercisesOptionsValidator : AbstractValidator<GetAllExercisesOptions>
    {
        private static readonly string[] AllowedSortFields = { "Name" };
        public GetAllExercisesOptionsValidator()
        {
            RuleFor(x => x.SortField)
                .Must(x => x is null || AllowedSortFields.Contains(x, StringComparer.OrdinalIgnoreCase)).WithMessage("You can only sort by 'Name'");
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1");
            RuleFor(x => x.PageSize).InclusiveBetween(1, 15).WithMessage("Page size must be 1 - 15");

        }
    }
}
