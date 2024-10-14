using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Services.Equipments
{
    internal class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IFileService _fileService;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentService(IEquipmentRepository equipmentRepository, IFileService fileService, IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _equipmentRepository = equipmentRepository;
            _fileService = fileService;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(Equipment equipment)
        {
            try
            {
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
                var equipment = await _equipmentRepository.EquipmentByIdAsync(id);
                if (equipment is null)
                {
                    return false;
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
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }

        public async Task<Equipment> EquipmentByIdAsync(Guid id)
        {
            return await _equipmentRepository.EquipmentByIdAsync(id);
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            return await _equipmentRepository.GetAllAsync();
        }

        public async Task<Equipment> UpdateAsync(Equipment equipment)
        {
            try
            {
                var equipmentExist = await _equipmentRepository.EquipmentByIdAsync(equipment.Id);
                if (equipmentExist is null)
                {
                    throw new KeyNotFoundException($"Muscle with id {equipment.Id} was not found.");
                }
                equipment.Name = equipment.Name;

                if (equipment.ImageFile is not null)
                {
                    var image = await _imageRepository.ImageByImageableIdAsync(equipment.Id);
                    if (image is not null)
                    {
                        string[] allowedFileExtensions = [".jpg", ".png"];
                        var typeName = typeof(Muscle).Name;
                        var createdImageId = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName, equipment.Id);
                        _fileService.DeleteFileAsync(image.ImagePath);
                        await _imageRepository.DeleteByIdAsync(image.Id);
                    }
                    else
                    {
                        string[] allowedFileExtensions = [".jpg", ".png"];
                        var typeName = typeof(Muscle).Name;
                        var createdImageId = await _fileService.SaveFileAsync(equipment.ImageFile, allowedFileExtensions, typeName, equipment.Id);
                    }
                }
                await _equipmentRepository.UpdateAsync(equipmentExist);
                await _unitOfWork.CommitChangesAsync();
                return equipmentExist;
            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }
    }
}
