using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Api.Models.Dtos.Exercises;
using TunzWorkout.Api.Models.Dtos.Levels;
using TunzWorkout.Api.Models.Dtos.Muscles;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Exercises;
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
        public static MusclesResponse MapToResponse(this IEnumerable<Muscle> muscles)
        {
            return new MusclesResponse
            {
                Items = muscles.Select(MapToResponse)
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
        public static EquipmentsResponse MapToResponse(this IEnumerable<Equipment> equipments)
        {
            return new EquipmentsResponse
            {
                Items = equipments.Select(MapToResponse)
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

        public static LevelsResponse MapToResponse(this IEnumerable<Level> levels)
        {
            return new LevelsResponse
            {
                Items = levels.Select(MapToResponse),
            };
        }

        #endregion

        #region Exercise

        public static Exercise MapToExercise(this CreateExerciseRequest request)
        {
            return new Exercise
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LevelId = request.LevelId,
                HasEquipment = request.HasEquipment,
                SelectedEquipmentIds = request.SelectedEquipmentIds,
                SelectedMuscleIds = request.SelectedMuscleIds,
            };
        }
        public static ExerciseResponse MapToResponse(this Exercise exercise)
        {
            return new ExerciseResponse
            {
                Id = exercise.Id,
                Name = exercise.Name,
                LevelId = exercise.LevelId,
                HasEquipment = exercise.HasEquipment,
                SelectedEquipmentIds = exercise.SelectedEquipmentIds,
                SelectedMuscleIds = exercise.SelectedMuscleIds,
            };
        }

        #endregion
    }
}
