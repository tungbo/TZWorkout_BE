using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.Images;
using TunzWorkout.Domain.Entities.Muscles;
using static System.Net.Mime.MediaTypeNames;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public class MuscleService : IMuscleService
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public MuscleService(IMuscleRepository muscleRepository, IUnitOfWork unitOfWork, IFileService fileService, IImageRepository imageRepository)
        {
            _muscleRepository = muscleRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _imageRepository = imageRepository;
        }

        public async Task<bool> CreateAsync(Muscle muscle)
        {
            try
            {
                if (muscle.ImageFile is not null)
                {
                    string[] allowedFileExtensions = [".jpg", ".png"];
                    var typeName = typeof(Muscle).Name;
                    var createdImageId = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName, muscle.Id);
                }
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
                var muscle = await _muscleRepository.MuscleByIdAsync(id);
                if (muscle is null)
                {
                    return false;
                }
                var image = await _imageRepository.ImageByImageableIdAsync(muscle.Id);
                if (image is not null)
                {
                    _fileService.DeleteFileAsync(image.ImagePath);

                    await _muscleRepository.DeleteByIdAsync(id);
                    await _imageRepository.DeleteByIdAsync(image.Id);
                    await _unitOfWork.CommitChangesAsync();
                } else
                {
                    await _muscleRepository.DeleteByIdAsync(id);
                    await _unitOfWork.CommitChangesAsync();
                }
                return true;
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
                if (muscleExist is null)
                {
                    throw new KeyNotFoundException($"Muscle with id {muscle.Id} was not found.");
                }
                muscleExist.Name = muscle.Name;

                if (muscle.ImageFile is not null)
                {
                    var image = await _imageRepository.ImageByImageableIdAsync(muscle.Id);
                    if (image is not null)
                    {
                        string[] allowedFileExtensions = [".jpg", ".png"];
                        var typeName = typeof(Muscle).Name;
                        var createdImageId = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName, muscle.Id);
                        _fileService.DeleteFileAsync(image.ImagePath);
                        await _imageRepository.DeleteByIdAsync(image.Id);
                    }
                }
                await _muscleRepository.UpdateAsync(muscleExist);
                await _unitOfWork.CommitChangesAsync();
                return muscleExist;
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }

            //throw new NotImplementedException();
        }
    }
}
