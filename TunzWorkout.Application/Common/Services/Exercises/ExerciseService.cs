using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Application.Common.Services.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMuscleRepository _muscleRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Exercise> _exerciseValidator;
        public ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork, IMuscleRepository muscleRepository, IEquipmentRepository equipmentRepository, IValidator<Exercise> exerciseValidator)
        {
            _exerciseRepository = exerciseRepository;
            _unitOfWork = unitOfWork;
            _muscleRepository = muscleRepository;
            _equipmentRepository = equipmentRepository;
            _exerciseValidator = exerciseValidator;
        }

        public async Task<ErrorOr<Exercise>> CreateAsync(Exercise exercise)
        {
            var validationResult = await _exerciseValidator.ValidateAsync(exercise);
            if(!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Error.Validation(description: string.Join(" & ", errorMessage));
            }

            foreach (var muscleId in exercise.SelectedMuscleIds)
            {
                var muscleExists = await _muscleRepository.ExistByIdAsync(muscleId);
                if (muscleExists)
                {
                    var exerciseMuscle = new ExerciseMuscle
                    {
                        ExerciseId = exercise.Id,
                        MuscleId = muscleId
                    };
                    exercise.ExerciseMuscles.Add(exerciseMuscle);
                }
                else
                {
                    return Error.NotFound("Muscle not found");
                }
            }

            // Thêm các mối quan hệ Exercise-Equipment vào bảng trung gian
            foreach (var equipmentId in exercise.SelectedEquipmentIds)
            {
                var equipmentExists = await _equipmentRepository.ExistByIdAsync(equipmentId);
                if (equipmentExists)
                {
                    var exerciseEquipment = new ExerciseEquipment
                    {
                        ExerciseId = exercise.Id,
                        EquipmentId = equipmentId
                    };
                    exercise.ExerciseEquipments.Add(exerciseEquipment);
                }
                else
                {
                    return Error.NotFound("Equipment not found");
                }
            }

            await _exerciseRepository.CreateAsync(exercise);
            await _unitOfWork.CommitChangesAsync();
            return exercise;
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Exercise> ExerciseByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exercise>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Exercise> UpdateAsync(Exercise exercise)
        {
            throw new NotImplementedException();
        }
    }

}
