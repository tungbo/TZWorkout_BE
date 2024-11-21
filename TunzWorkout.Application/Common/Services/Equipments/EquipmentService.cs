using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.EquipmentImages;
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Services.Equipments
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentImageRepository _equipmentImageRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Equipment> _equipmentValidator;

        public EquipmentService(IEquipmentRepository equipmentRepository, IFileService fileService, IEquipmentImageRepository equipmentImageRepository, IUnitOfWork unitOfWork, IValidator<Equipment> equipmentValidator)
        {
            _equipmentRepository = equipmentRepository;
            _fileService = fileService;
            _equipmentImageRepository = equipmentImageRepository;
            _unitOfWork = unitOfWork;
            _equipmentValidator = equipmentValidator;
        }

        public async Task<ErrorOr<Equipment>> CreateAsync(Equipment equipment)
        {
            var validationResult = await _equipmentValidator.ValidateAsync(equipment);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            if (await _equipmentRepository.ExistByNameAsync(equipment.Name))
            {
                return Error.Conflict(description: "Equipment exist.");
            }

            if (equipment.ImageFile is not null)
            {
                //const int maxFileSizeInBytes = 2 * 1024 * 1024; // 2MB
                //if (equipment.ImageFile.Length > maxFileSizeInBytes)
                //{
                //    throw new Exception("The image file size exceeds the 2MB limit.");
                //}
                string[] allowedFileExtensions = [".jpg", ".png"];
                var typeName = typeof(Equipment).Name;
                var result = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName);
                if (result.IsError)
                {
                    return result.Errors;
                }
                var image = new EquipmentImage
                {
                    Id = Guid.NewGuid(),
                    ImagePath = result.Value,
                    UploadDate = DateTime.Now,
                    EquipmentId = equipment.Id
                };
                await _equipmentImageRepository.CreateAsync(image);
            }
            await _equipmentRepository.CreateAsync(equipment);
            await _unitOfWork.CommitChangesAsync();

            var createdEquipment = await _equipmentRepository.EquipmentByIdAsync(equipment.Id);
            return createdEquipment is not null ? createdEquipment : Error.NotFound(description: "Equipment not found.");
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            var equipment = await _equipmentRepository.EquipmentByIdAsync(id);
            if (equipment is null)
            {
                return Error.NotFound(description: "Equipment not found.");
            }
            var image = await _equipmentImageRepository.GetEquipmentImageByEquipmentIdAsync(equipment.Id);
            if (image is not null)
            {
                var result = _fileService.DeleteFileAsync(image.ImagePath);
                if (result.IsCompleted)
                {
                    await _equipmentRepository.DeleteByIdAsync(id);
                    await _unitOfWork.CommitChangesAsync();
                }
                else
                {
                    return Error.Conflict(description: "Error while deleting image.");
                }
            }
            else
            {
                await _equipmentRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitChangesAsync();
            }
            return Result.Deleted;
        }

        public async Task<ErrorOr<Equipment>> EquipmentByIdAsync(Guid id)
        {
            var equipment = await _equipmentRepository.EquipmentByIdAsync(id);
            if (equipment is null)
            {
                return Error.NotFound(description: "Equipment not found.");
            }
            return equipment;
        }

        public async Task<ErrorOr<IEnumerable<Equipment>>> GetAllAsync()
        {
            var equipments = await _equipmentRepository.GetAllAsync();
            if (equipments is null || !equipments.Any())
            {
                return Error.NotFound(description: "Equipment not found.");
            }

            return equipments.ToList();
        }

        public async Task<ErrorOr<Equipment>> UpdateAsync(Equipment equipment)
        {
            var validationResult = await _equipmentValidator.ValidateAsync(equipment);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            var equipmentExist = await _equipmentRepository.EquipmentByIdAsync(equipment.Id);
            if (equipmentExist is null)
            {
                return Error.NotFound(description: "Equipment not found.");
            }

            var equipmentWithSameName = await _equipmentRepository.EquipmentByNameAsync(equipment.Name);
            if (equipmentWithSameName != null && equipmentWithSameName.Id != equipment.Id)
            {
                return Error.Conflict(description: "Equipment name already exists");
            }
            equipmentExist.Name = equipment.Name;

            if (equipment.ImageFile is not null)
            {
                var image = await _equipmentImageRepository.GetEquipmentImageByEquipmentIdAsync(equipment.Id);
                if (image is not null)
                {
                    var deleteResult = _fileService.DeleteFileAsync(image.ImagePath);
                    if (deleteResult.IsCompleted)
                    {
                        await _equipmentImageRepository.DeleteByIdAsync(image.Id);
                    }
                    else
                    {
                        return Error.Conflict(description: "Error while deleting image.");
                    }
                }
                string[] allowedFileExtensions = [".jpg", ".png"];
                var typeName = typeof(Equipment).Name;
                var result = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName);
                if (result.IsError)
                {
                    return result.Errors;
                }
                var equipmentImage = new EquipmentImage
                {
                    Id = Guid.NewGuid(),
                    ImagePath = result.Value,
                    UploadDate = DateTime.Now,
                    EquipmentId = equipment.Id
                };
                await _equipmentImageRepository.CreateAsync(equipmentImage);
            }
            await _equipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.CommitChangesAsync();
            var updatedEquipment = await _equipmentRepository.EquipmentByIdAsync(equipment.Id);
            return updatedEquipment is not null ? updatedEquipment : Error.NotFound(description: "Equipment not found.");
        }
    }
}
