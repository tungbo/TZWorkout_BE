using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.MuscleImages;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Muscles
{
    public class MuscleService : IMuscleService
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IMuscleImageRepository _muscleImageRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Muscle> _muscleValidator;

        public MuscleService(IMuscleRepository muscleRepository, IUnitOfWork unitOfWork, IFileService fileService, IMuscleImageRepository muscleImageRepository, IValidator<Muscle> muscleValidator)
        {
            _muscleRepository = muscleRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _muscleImageRepository = muscleImageRepository;
            _muscleValidator = muscleValidator;
        }

        public async Task<ErrorOr<Muscle>> CreateAsync(Muscle muscle)
        {
            var validationResult = await _muscleValidator.ValidateAsync(muscle);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            if (await _muscleRepository.ExistByNameAsync(muscle.Name))
            {
                return Error.Conflict(description: "Muscle exist.");
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
                var result = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName);
                if (result.IsError)
                {
                    return result.Errors;
                }
                var muscleImage = new MuscleImage
                {
                    Id = Guid.NewGuid(),
                    ImagePath = result.Value,
                    UploadDate = DateTime.Now,
                    MuscleId = muscle.Id
                };
                await _muscleImageRepository.CreateAsync(muscleImage);
            }
            await _muscleRepository.CreateAsync(muscle);
            await _unitOfWork.CommitChangesAsync();
            var createdMuscle = await _muscleRepository.MuscleByIdAsync(muscle.Id);
            return createdMuscle is not null ? createdMuscle : Error.NotFound(description: "Muscle not found.");
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            var muscle = await _muscleRepository.MuscleByIdAsync(id);
            if (muscle is null)
            {
                return Error.NotFound(description: "Muscle not found.");
            }
            var image = await _muscleImageRepository.GetMuscleImageByMuscleIdAsync(muscle.Id);
            if (image is not null)
            {
                var result = _fileService.DeleteFileAsync(image.ImagePath);
                if (result.IsCompleted)
                {
                    await _muscleRepository.DeleteByIdAsync(id);
                    await _unitOfWork.CommitChangesAsync();
                }
                else
                {
                    return Error.Conflict(description: "Error deleting image file.");
                }
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
                return Error.NotFound(description: "No muscles found.");
            }
            return muscles.ToList();
        }

        public async Task<ErrorOr<Muscle>> MuscleByIdAsync(Guid id)
        {
            var muscle = await _muscleRepository.MuscleByIdAsync(id);
            if (muscle is null)
            {
                return Error.NotFound(description: "Muscle not found");
            }
            return muscle;
        }

        public async Task<ErrorOr<Muscle>> UpdateAsync(Muscle muscle)
        {
            var validationResult = await _muscleValidator.ValidateAsync(muscle);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            var muscleExist = await _muscleRepository.MuscleByIdAsync(muscle.Id);
            if (muscleExist is null)
            {
                return Error.NotFound(description: "Muscle not found");
            }
            var muscleWithSameName = await _muscleRepository.MuscleByNameAsync(muscle.Name);
            if (muscleWithSameName is not null && muscleWithSameName.Id != muscle.Id)
            {
                return Error.Conflict(description: "Muscle name already exists");
            }

            if (muscle.ImageFile is not null)
            {
                var image = await _muscleImageRepository.GetMuscleImageByMuscleIdAsync(muscle.Id);
                if (image is not null)
                {
                    var deletedResult = _fileService.DeleteFileAsync(image.ImagePath);
                    if (deletedResult.IsCompleted)
                    {
                        await _muscleImageRepository.DeleteByIdAsync(image.Id);
                    }
                    else
                    {
                        return Error.Conflict(description: "Error while deleting image.");
                    }
                }
                string[] allowedFileExtensions = [".jpg", ".png"];
                var typeName = typeof(Muscle).Name;
                var result = await _fileService.SaveFileAsync(muscle.ImageFile, allowedFileExtensions, typeName);
                if (result.IsError)
                {
                    return result.Errors;
                }
                var muscleImage = new MuscleImage
                {
                    Id = Guid.NewGuid(),
                    ImagePath = result.Value,
                    UploadDate = DateTime.Now,
                    MuscleId = muscle.Id
                };
                await _muscleImageRepository.CreateAsync(muscleImage);
            }
            await _muscleRepository.UpdateAsync(muscle);
            await _unitOfWork.CommitChangesAsync();
            var updatedMuscle = await _muscleRepository.MuscleByIdAsync(muscle.Id);
            return updatedMuscle is not null ? updatedMuscle : Error.NotFound("Muscle not found");
        }
    }
}
