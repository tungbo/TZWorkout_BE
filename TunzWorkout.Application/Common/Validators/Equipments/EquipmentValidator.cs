using FluentValidation;
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Validators.Equipments
{
    public class EquipmentValidator : AbstractValidator<Equipment>
    {
        public EquipmentValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Matches(@"^[^\d]+$");
        }
    }
}
