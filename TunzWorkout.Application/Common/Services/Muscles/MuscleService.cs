using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public class MuscleService : IMuscleService
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Muscle> _muscleValidator;

        public MuscleService(IMuscleRepository muscleRepository, IUnitOfWork unitOfWork, IFileService fileService, IImageRepository imageRepository, IValidator<Muscle> muscleValidator)
        {
            _muscleRepository = muscleRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _imageRepository = imageRepository;
            _muscleValidator = muscleValidator;
        }

        public async Task<ErrorOr<Muscle>> CreateAsync(Muscle muscle)
        {
            var validationResult = await _muscleValidator.ValidateAsync(muscle);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Error.Validation(description: string.Join(" & ", errorMessages));
            }

            if (await _muscleRepository.ExistByNameAsync(muscle.Name))
            {
                return Error.Conflict(description:"Muscle exist.");
            }

            if (muscle.ImageFile is not null)
            {
                //const int maxFileSizeInBytes = 2 * 1024 * 1024; // 2MB
                //if (muscle.ImageFile.Length > maxFileSizeInBytes)
                //{
                //    throw new Exception("The image file size exceeds the 2MB limit.");
                //}
                string[] allowedFileExtensions = [".jpg", ".png"];
                var typeName = typeof(Muscle).Name;
                var createdImageId = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName, muscle.Id);
            }
            await _muscleRepository.CreateAsync(muscle);
            await _unitOfWork.CommitChangesAsync();
            return muscle;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            var muscle = await _muscleRepository.MuscleByIdAsync(id);
            if (muscle is null)
            {
                return Error.NotFound(description:"Muscle not found.");
            }
            var image = await _imageRepository.ImageByImageableIdAsync(muscle.Id);
            if (image is not null)
            {
                _fileService.DeleteFileAsync(image.ImagePath);

                await _muscleRepository.DeleteByIdAsync(id);
                await _imageRepository.DeleteByIdAsync(image.Id);
                await _unitOfWork.CommitChangesAsync();
            }
            else
            {
                await _muscleRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<IEnumerable<Muscle>>> GetAllAsync()
        {
            var muscles = await _muscleRepository.GetAllAsync();
            if (muscles is null || !muscles.Any())
            {
                return Error.NotFound(description:"No muscles found.");
            }
            return muscles.ToList();
        }

        public async Task<ErrorOr<Muscle>> MuscleByIdAsync(Guid id)
        {
            var muscle = await _muscleRepository.MuscleByIdAsync(id);
            if (muscle is null)
            {
                return Error.NotFound(description:"Muscle not found");
            }
            return muscle;
        }

        public async Task<ErrorOr<Muscle>> UpdateAsync(Muscle muscle)
        {
            var validationResult = await _muscleValidator.ValidateAsync(muscle);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Error.Validation(description: string.Join(" & ", errorMessages));
            }

            var muscleExist = await _muscleRepository.MuscleByIdAsync(muscle.Id);
            if (muscleExist is null)
            {
                return Error.NotFound(description:"Muscle not found");
            }
            var muscleWithSameName = await _muscleRepository.MuscleByNameAsync(muscle.Name);
            if (muscleWithSameName != null && muscleWithSameName.Id != muscle.Id)
            {
                return Error.Conflict(description: "Muscle name already exists");
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
                else
                {
                    string[] allowedFileExtensions = [".jpg", ".png"];
                    var typeName = typeof(Muscle).Name;
                    var createdImageId = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName, muscle.Id);
                }
            }
            await _muscleRepository.UpdateAsync(muscleExist);
            await _unitOfWork.CommitChangesAsync();
            return muscleExist;
        }
    }
}
