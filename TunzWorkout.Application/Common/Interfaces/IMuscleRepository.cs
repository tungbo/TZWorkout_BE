﻿using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IMuscleRepository
    {
        Task<bool> CreateAsync(Muscle muscle);
        Task<bool> UpdateAsync(Muscle muscle);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistByIdAsync(Guid id);

        Task<Muscle> MuscleByIdAsync(Guid id);
        Task<IEnumerable<Muscle>> GetAllAsync();
    }
}