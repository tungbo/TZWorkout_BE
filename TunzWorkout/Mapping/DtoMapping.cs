using TunzWorkout.Api.Models.Dto.Equipments;
using TunzWorkout.Api.Models.Dto.Muscles;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Api.Mapping
{
    public static class DtoMapping
    {
        #region Muscle
        public static Muscle MapToMuscle(this CreateMuscleRequest request)
        {
            return new Muscle
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageFile = request.ImageFile,
            };
        }
        public static Muscle MapToMuscle(this UpdateMuscleRequest request, Guid id)
        {
            return new Muscle
            {
                Id = id,
                Name = request.Name,
                ImageFile = request.ImageFile,
            };
        }
        public static MuscleDTO MapToMuscleDTO(this Muscle muscle)
        {
            return new MuscleDTO
            {
                Name = muscle.Name,
            };
        }
        public static MuscleResponse MapToResponse(this Muscle muscle)
        {
            return new MuscleResponse
            {
                Id = muscle.Id,
                Name = muscle.Name,
            };
        }
        #endregion

        #region Equipment

        public static Equipment MapToEquipment(this CreateEquipmentRequest request)
        {
            return new Equipment
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageFile = request.ImageFile
            };
        }
        public static Equipment MapToEquipment(this UpdateEquipmentRequest request, Guid id)
        {
            return new Equipment
            {
                Id = id,
                Name = request.Name,
                ImageFile = request.ImageFile
            };
        }
        public static EquipmentResponse MapToResponse(this Equipment equipment)
        {
            return new EquipmentResponse
            {
                Id = equipment.Id,
                Name = equipment.Name,
            };
        }

        #endregion
    }
}
