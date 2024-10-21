using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Application.Common.Services.Equipments
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IFileService _fileService;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Equipment> _equipmentValidator;

        public EquipmentService(IEquipmentRepository equipmentRepository, IFileService fileService, IImageRepository imageRepository, IUnitOfWork unitOfWork, IValidator<Equipment> equipmentValidator)
        {
            _equipmentRepository = equipmentRepository;
            _fileService = fileService;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
            _equipmentValidator = equipmentValidator;
        }

        public async Task<ErrorOr<Equipment>> CreateAsync(Equipment equipment)
        {
            var validationResult = await _equipmentValidator.ValidateAsync(equipment);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Error.Validation(description: string.Join(" & ", errorMessages));
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
                var createdImageId = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName, equipment.Id);
            }
            await _equipmentRepository.CreateAsync(equipment);
            await _unitOfWork.CommitChangesAsync();
            return equipment;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            var equipment = await _equipmentRepository.EquipmentByIdAsync(id);
            if (equipment is null)
            {
                return Error.NotFound(description: "Equipment not found.");
            }
            var image = await _imageRepository.ImageByImageableIdAsync(equipment.Id);
            if (image is not null)
            {
                _fileService.DeleteFileAsync(image.ImagePath);

                await _equipmentRepository.DeleteByIdAsync(id);
                await _imageRepository.DeleteByIdAsync(image.Id);
                await _unitOfWork.CommitChangesAsync();
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
            if (equipments is null | !equipments.Any())
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
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Error.Validation(description: string.Join(" & ", errorMessages));
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
                var image = await _imageRepository.ImageByImageableIdAsync(equipment.Id);
                if (image is not null)
                {
                    string[] allowedFileExtensions = [".jpg", ".png"];
                    var typeName = typeof(Equipment).Name;
                    var createdImageId = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName, equipment.Id);
                    _fileService.DeleteFileAsync(image.ImagePath);
                    await _imageRepository.DeleteByIdAsync(image.Id);
                }
                else
                {
                    string[] allowedFileExtensions = [".jpg", ".png"];
                    var typeName = typeof(Equipment).Name;
                    var createdImageId = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName, equipment.Id);
                }
            }
            await _equipmentRepository.UpdateAsync(equipmentExist);
            await _unitOfWork.CommitChangesAsync();
            return equipmentExist;
        }
    }
}
