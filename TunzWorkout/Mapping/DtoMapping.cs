using TunzWorkout.Api.Models.Dto.Muscles;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Api.Mapping
{
    public static class DtoMapping
    {
        public static Muscle MapToMuscle(this CreateMuscleRequest request)
        {
            return new Muscle
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageFile = request.ImageFile,
            };
        }
        public static Muscle MapToMuscle(this CreateMuscleRequest request, Guid id)
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
    }
}
