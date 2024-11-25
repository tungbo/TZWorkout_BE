using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Filters;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Files;
using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.Videos;

namespace TunzWorkout.Application.Common.Services.Exercises
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMuscleRepository _muscleRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoFileService _videoFileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExerciseEquipmentRepository _exerciseEquipmentRepository;
        private readonly IExerciseMuscleRepository _exerciseMuscleRepository;
        private readonly IValidator<Exercise> _exerciseValidator;
        private readonly IValidator<GetAllExercisesOptions> _allExercisesOptionsValidator;
        public ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork, IMuscleRepository muscleRepository, IEquipmentRepository equipmentRepository, IVideoRepository videoRepository, IValidator<Exercise> exerciseValidator, IValidator<GetAllExercisesOptions> getAllExOpValidator, IVideoFileService videoFileService, ILevelRepository levelRepository, IExerciseEquipmentRepository exerciseEquipmentRepository, IExerciseMuscleRepository exerciseMuscleRepository)
        {
            _exerciseMuscleRepository = exerciseMuscleRepository;
            _exerciseEquipmentRepository = exerciseEquipmentRepository;
            _exerciseRepository = exerciseRepository;
            _unitOfWork = unitOfWork;
            _muscleRepository = muscleRepository;
            _equipmentRepository = equipmentRepository;
            _exerciseValidator = exerciseValidator;
            _allExercisesOptionsValidator = getAllExOpValidator;
            _videoRepository = videoRepository;
            _videoFileService = videoFileService;
            _levelRepository = levelRepository;
        }

        public async Task<ErrorOr<Exercise>> CreateAsync(Exercise exercise)
        {
            var validationResult = await _exerciseValidator.ValidateAsync(exercise);
            if(!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            if(!await _levelRepository.ExistByIdAsync(exercise.LevelId))
            {
                return Error.NotFound(description: "Level not found");
            }

            #region Check and add muscle and equipment
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
                    return Error.NotFound(description: "Muscle not found");
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
                    return Error.NotFound(description: "Equipment not found");
                }
            }
            #endregion

            if (exercise.VideoFile is not null)
            {
                string[] allowedVideoExtension = [".mp4"];
                var typeName = typeof(Exercise).Name;
                var result = await _videoFileService.SaveVideoAsync(exercise.VideoFile, allowedVideoExtension, typeName);
                if(result.IsError)
                {
                    return result.Errors;
                }

                Video video = new Video
                {
                    Id = Guid.NewGuid(),
                    VideoPath = result.Value.ToString(),
                    UploadDate = DateTime.Now,
                    ExerciseId = exercise.Id
                };
                //video ? add : remove videFile
                await _videoRepository.CreateAsync(video);
            }
            await _exerciseRepository.CreateAsync(exercise);
            await _unitOfWork.CommitChangesAsync();

            var createdExercise = await _exerciseRepository.ExerciseByIdAsync(exercise.Id);
            return createdExercise is not null ? createdExercise : Error.NotFound(description: "Exercise not found");
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(Guid id)
        {
            if (!await _exerciseRepository.ExistByIdAsync(id))
            {
                return Error.NotFound(description: "Exercise not found");
            }
            var video = await _videoRepository.VideoByExerciseIdAsync(id);
            if (video is not null)
            {
                var result = _videoFileService.DeleteVideoAsync(video.VideoPath);
                if(!result.IsCompleted)
                {
                    return Error.Conflict(description: "Error while deleting video");
                }
            }
            await _exerciseRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<Exercise>> ExerciseByIdAsync(Guid id)
        {
            var exercise =  await _exerciseRepository.ExerciseByIdAsync(id);
            if(exercise is null)
            {
                return Error.NotFound(description: "exercise not found");
            }
            return exercise;
        }

        public async Task<ErrorOr<IEnumerable<Exercise>>> GetAllAsync(GetAllExercisesOptions options)
        {
            var validationResult = await _allExercisesOptionsValidator.ValidateAsync(options);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var exercises = await _exerciseRepository.GetAllAsync(options);
            if(exercises is null||!exercises.Any())
            {
                return Error.NotFound(description: "No exercise found");
            }
            return exercises.ToList();
        }

        public async Task<ErrorOr<Exercise>> UpdateAsync(Exercise exercise)
        {
            var validationResult = await _exerciseValidator.ValidateAsync(exercise);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var existingExercise = await _exerciseRepository.ExerciseByIdAsync(exercise.Id);
            if (existingExercise is null)
            {
                return Error.NotFound(description: "Level not found");
            }
            existingExercise.Name = exercise.Name;
            existingExercise.LevelId = exercise.LevelId;
            existingExercise.HasEquipment = exercise.HasEquipment;

            #region Check and update muscle and equipment
            foreach (var muscleId in exercise.SelectedMuscleIds)
            {
                if (!await _muscleRepository.ExistByIdAsync(muscleId))
                {
                    return Error.NotFound(description: "Muscle not found");
                }
            }

            foreach (var equipmentId in exercise.SelectedEquipmentIds)
            {
                if (!await _equipmentRepository.ExistByIdAsync(equipmentId))
                {
                    return Error.NotFound(description: "Equipment not found");
                }
            }

            await UpdateExerciseMuscles(exercise);
            await UpdateExerciseEquipments(exercise);
            #endregion

            #region Check and update video
            if (exercise.VideoFile is not null)
            {
                var existingVideo = await _videoRepository.VideoByExerciseIdAsync(exercise.Id);
                if (existingVideo is not null)
                {
                    var deleteVideoResult = _videoFileService.DeleteVideoAsync(existingVideo.VideoPath);
                    if(!deleteVideoResult.IsCompleted)
                    {
                        return Error.Conflict(description: "Error while deleting video");
                    }
                    await _videoRepository.DeleteByIdAsync(existingVideo.Id);
                }

                string[] allowedVideoExtension = [".mp4"];
                var typeName = typeof(Exercise).Name;
                var result = await _videoFileService.SaveVideoAsync(exercise.VideoFile, allowedVideoExtension, typeName);
                if (result.IsError)
                {
                    return result.Errors;
                }

                Video video = new Video
                {
                    Id = Guid.NewGuid(),
                    VideoPath = result.Value.ToString(),
                    UploadDate = DateTime.Now,
                    ExerciseId = exercise.Id
                };
                // create video ? keep : remove videFile
                await _videoRepository.CreateAsync(video);
            }
            #endregion

            await _exerciseRepository.UpdateAsync(existingExercise);
            await _unitOfWork.CommitChangesAsync();
            var updatedExercise = await _exerciseRepository.ExerciseByIdAsync(exercise.Id);
            return updatedExercise is not null ? updatedExercise : Error.NotFound(description: "Exercise not found");
        }

        private async Task UpdateExerciseMuscles(Exercise exercise)
        {
            var existingMuscles = await _exerciseMuscleRepository.GetByExerciseIdAsync(exercise.Id);
            var existingMuscleIds = existingMuscles.Select(em => em.MuscleId).ToList();
            // Select muscleIds not in selectedMuscleIds
            var musclesToRemove = existingMuscles.Where(em => !exercise.SelectedMuscleIds.Contains(em.MuscleId)).ToList();
            if (musclesToRemove.Any())
            {
                await _exerciseMuscleRepository.DeleteRangeAsync(musclesToRemove);
                await _unitOfWork.CommitChangesAsync();
            }
            // Select muscleIds in selectedMuscleIds not in existingMuscleIds
            var newMuscleIds = exercise.SelectedMuscleIds.Except(existingMuscleIds).ToList();
            var musclesToAdd = newMuscleIds.Select(muscleId => new ExerciseMuscle
            {
                ExerciseId = exercise.Id,
                MuscleId = muscleId
            }).ToList();

            if (musclesToAdd.Any())
            {
                await _exerciseMuscleRepository.AddRangeAsync(musclesToAdd);
            }
        }
        private async Task UpdateExerciseEquipments(Exercise exercise)
        {
            var existingEquipments = await _exerciseEquipmentRepository.GetByExerciseIdAsync(exercise.Id);
            var existingEquipmentIds = existingEquipments.Select(ee => ee.EquipmentId).ToList();
            var equipmentsToRemove = existingEquipments.Where(ee => !exercise.SelectedEquipmentIds.Contains(ee.EquipmentId)).ToList();
            if (equipmentsToRemove.Any())
            {
                await _exerciseEquipmentRepository.DeleteRangeAsync(equipmentsToRemove);
                await _unitOfWork.CommitChangesAsync();
            }
            var newEquipmentIds = exercise.SelectedEquipmentIds.Except(existingEquipmentIds).ToList();
            var equipmentsToAdd = newEquipmentIds.Select(equipmentId => new ExerciseEquipment
            {
                ExerciseId = exercise.Id,
                EquipmentId = equipmentId
            }).ToList();

            if (equipmentsToAdd.Any())
            {
                await _exerciseEquipmentRepository.AddRangeAsync(equipmentsToAdd);
            }
        }

        public async Task<int> CountAsync(string? name)
        {
            return await _exerciseRepository.CountAsync(name);
        }
    }


}
