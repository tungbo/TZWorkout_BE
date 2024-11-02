using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Application.Common.Services.Levels
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Level> _levelValidator;

        public LevelService(ILevelRepository levelRepository, IUnitOfWork unitOfWork, IValidator<Level> levelValidator)
        {
            _levelRepository = levelRepository;
            _unitOfWork = unitOfWork;
            _levelValidator = levelValidator;
        }

        public async Task<ErrorOr<Level>> CreateAsync(Level level)
        {
            var validationResult = await _levelValidator.ValidateAsync(level);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            if (await _levelRepository.ExistByIdName(level.Name)) 
            {
                return Error.Conflict(description: "Level exist");
            }

            await _levelRepository.CreateAsync(level);
            await _unitOfWork.CommitChangesAsync();
            return level;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            if (!await _levelRepository.ExistByIdAsync(id)) { return Error.NotFound(description: "Level not found"); }

            await _levelRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<IEnumerable<Level>>> GetAllAsync()
        {
            var levels = await _levelRepository.GetAllAsync();
            if (levels is null || !levels.Any())
            {
                return Error.NotFound(description: "No levels found.");
            }
            return levels.ToList();
        }

        public async Task<ErrorOr<Level>> GetLevelByIdAsync(Guid id)
        {
            var level = await _levelRepository.LevelByIdAsync(id);

            if (level is null)
            {
                return Error.NotFound(description: "Level not found");
            }
            return level;
        }

        public async Task<ErrorOr<Level>> UpdateAsync(Level level)
        {
            var validationResult = await _levelValidator.ValidateAsync(level);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }

            var existingLevel = await _levelRepository.LevelByIdAsync(level.Id);
            if (existingLevel is null)
            {
                return Error.NotFound(description: "Level not found");
            }
            var levelWithSameName = await _levelRepository.LevelByNameAsync(level.Name);
            if (levelWithSameName != null && levelWithSameName.Id != level.Id)
            {
                return Error.Conflict(description: "Level name already exists");
            }

            existingLevel.Name = level.Name;
            existingLevel.Description = level.Description;

            await _levelRepository.UpdateAsync(existingLevel);
            await _unitOfWork.CommitChangesAsync();
            return existingLevel;
        }
    }
}
