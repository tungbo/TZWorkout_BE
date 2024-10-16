using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Application.Common.Services.Levels
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LevelService(ILevelRepository levelRepository, IUnitOfWork unitOfWork)
        {
            _levelRepository = levelRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(Level level)
        {
            try
            {
                if(await _levelRepository.ExistByIdAsync(level.Id))
                {
                    return false;
                }

                await _levelRepository.CreateAsync(level);
                await _unitOfWork.CommitChangesAsync();
                return true;

            }catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                var level = await _levelRepository.LevelByIdAsync(id);
                if (level is null)
                {
                    return false;
                }

                await _levelRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }

        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            return await _levelRepository.GetAllAsync();
        }

        public Task<Level> LevelByIdAsync(Guid id)
        {
            return _levelRepository.LevelByIdAsync(id);
        }

        public async Task<Level> UpdateAsync(Level level)
        {
            try
            {
                var levelExist = await _levelRepository.LevelByIdAsync(level.Id);
                if (levelExist is null)
                {
                    throw new KeyNotFoundException($"Level with id {level.Id} was not found.");
                }
                levelExist.Name = level.Name;
                levelExist.Description = level.Description;

                await _levelRepository.UpdateAsync(levelExist);
                await _unitOfWork.CommitChangesAsync();
                return levelExist;

            }
            catch (Exception ex)
            {
                throw new Exception("Error. Please try again later.", ex);
            }
        }
    }
}
