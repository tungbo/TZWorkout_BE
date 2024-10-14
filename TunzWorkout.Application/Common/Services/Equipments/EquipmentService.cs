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
                    //if (muscle.ImageFile.Length > maxFileSizeInBytes)
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

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Equipment> EquipmentByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Equipment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Equipment> UpdateAsync(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
