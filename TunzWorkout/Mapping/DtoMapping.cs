using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Api.Models.Dtos.Levels;
using TunzWorkout.Api.Models.Dtos.Muscles;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Levels;
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

        #region Level

        public static Level MapToLevel(this CreateLevelRequest request)
        {
            return new Level
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            };
        }

        public static Level MapToLevel(this UpdateLevelRequest request, Guid id)
        {
            return new Level
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
            };
        }

        public static LevelResponse MapToResponse(this Level level)
        {
            return new LevelResponse
            {
                Id = level.Id,
                Name = level.Name,
                Description = level.Description,
            };
        }

        #endregion
    }
}
