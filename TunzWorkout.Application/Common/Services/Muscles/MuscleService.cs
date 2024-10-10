using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public class MuscleService : IMuscleService
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public MuscleService(IMuscleRepository muscleRepository, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _muscleRepository = muscleRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(Muscle muscle)
        {
            try
            {
                string[] allowedFileExtensions = [".jpg", ".png"];
                var createdImageId = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions);
                muscle.ImageId = createdImageId;
                await _muscleRepository.CreateAsync(muscle);
                await _unitOfWork.CommitChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                if(await _muscleRepository.ExistByIdAsync(id))
                {
                    await _muscleRepository.DeleteByIdAsync(id);
                    await _unitOfWork.CommitChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }

        public async Task<IEnumerable<Muscle>> GetAllAsync()
        {
            return await _muscleRepository.GetAllAsync();
        }

        public async Task<Muscle> MuscleByIdAsync(Guid id)
        {
            return await _muscleRepository.MuscleByIdAsync(id);
        }

        public async Task<Muscle> UpdateAsync(Muscle muscle)
        {
            try
            {
                var muscleExist = await _muscleRepository.MuscleByIdAsync(muscle.Id);
                if(muscleExist is null)
                {
                    throw new KeyNotFoundException($"Muscle with id {muscle.Id} was not found.");
                }
                muscleExist.Name = muscle.Name;
                muscleExist.ImageId = muscle.ImageId;

                await _muscleRepository.UpdateAsync(muscleExist);
                await _unitOfWork.CommitChangesAsync();
                return muscleExist;
            } catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }
    }
}
