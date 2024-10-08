using TunzWorkout.Api.Models.Dto.Muscles;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Api.Mapping
{
    public static class DtoMapping
    {
        public static Muscle MapToMuscle(this MuscleDTO dto)
        {
            return new Muscle
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ImageId = dto.ImageId,
            };
        }
        public static Muscle MapToMuscle(this MuscleDTO dto, Guid id)
        {
            return new Muscle
            {
                Id = id,
                Name = dto.Name,
                ImageId = dto.ImageId,
            };
        }
        public static MuscleDTO MapToMuscleDTO(this Muscle muscle)
        {
            return new MuscleDTO
            {
                Name = muscle.Name,
                ImageId = muscle.ImageId,
            };
        }
        public static MuscleDTO MapToResponse(this Muscle muscle)
        {
            return new MuscleDTO
            {
                Id = muscle.Id,
                Name = muscle.Name,
                ImageId = muscle.ImageId,
            };
        }
    }
}
