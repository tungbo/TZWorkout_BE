using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.MuscleImages;

namespace TunzWorkout.Application.Common.Services.MuscleImages
{
    public class MuscleImageService : IMuscleImageService
    {
        private readonly IMuscleImageRepository _muscleImageRepository;
        public MuscleImageService(IMuscleImageRepository muscleImageRepository)
        {
            _muscleImageRepository = muscleImageRepository;
        }
        public Task CreateAsync(MuscleImage muscleImage)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
