using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Application.Common.Services.Rounds;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Application.Common.Services.Workouts
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IRoundExerciseRepository _roundExerciseRepository;
        private readonly IRoundService _roundService;
        private readonly IValidator<Workout> _workoutValidator;
        private readonly IValidator<Round> _roundValidator;
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutService(IWorkoutRepository workoutRepository, IRoundRepository roundRepository, IRoundExerciseRepository roundExerciseRepository, IRoundService roundService, IUnitOfWork unitOfWork, IValidator<Workout> workoutValidator, IValidator<Round> roundValidator)
        {
            _workoutRepository = workoutRepository;
            _roundRepository = roundRepository;
            _roundExerciseRepository = roundExerciseRepository;
            _roundService = roundService;
            _unitOfWork = unitOfWork;
            _workoutValidator = workoutValidator;
            _roundValidator = roundValidator;
        }
        public async Task<ErrorOr<Workout>> CreateAsync(Workout workout)
        {
            var validationResult = await _workoutValidator.ValidateAsync(workout);
            if(!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            foreach(var round in workout.Rounds)
            {
                round.WorkoutId = workout.Id;
                var roundCreateResult = await _roundService.CreateAsync(round);
                if(roundCreateResult.IsError)
                {
                    return roundCreateResult.Errors;
                }
            }
            await _workoutRepository.CreateAsync(workout);
            await _unitOfWork.CommitChangesAsync();
            return workout;
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id)
        {
            var workout = await _workoutRepository.WorkoutByIdAsync(id);
            if(workout is null)
            {
                return Error.NotFound(description: "Workout not found");
            }
            var rounds = await _roundRepository.GetAllByWorkoutIdAsync(workout.Id);
            foreach (var round in rounds)
            {
                await _roundExerciseRepository.DeleteAllByRoundId(round.Id);
                await _roundRepository.DeleteByIdAsync(round.Id);
            }
            await _workoutRepository.DeleteByIdAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public async Task<ErrorOr<IEnumerable<Workout>>> GetAllAsync()
        {
            var workouts = await _workoutRepository.GetAllAsync();
            if(workouts is null || !workouts.Any())
            {
                return Error.NotFound(description: "No workout found");
            }
            return workouts.ToList();
        }

        public async Task<ErrorOr<Workout?>> GetByIdAsync(Guid id)
        {
            var workout = await _workoutRepository.WorkoutByIdAsync(id);
            if(workout is null)
            {
                return Error.NotFound(description: "Workout not found");
            }
            return workout;
        }

        public async Task<ErrorOr<Workout>> UpdateAsync(Workout workout)
        {
            var validationResult = await _workoutValidator.ValidateAsync(workout);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var existingWorkout = await _workoutRepository.WorkoutByIdAsync(workout.Id);
            if (existingWorkout is null)
            {
                return Error.NotFound(description: "Workout not found");
            }
            var existingRounds = await _roundRepository.GetAllByWorkoutIdAsync(workout.Id);

            foreach (var round in workout.Rounds)
            {
                if(existingRounds.Any(x => x.Id == round.Id))
                {
                    var roundUpdateResult = await _roundService.UpdateAsync(round);
                    if (roundUpdateResult.IsError)
                    {
                        return roundUpdateResult.Errors;
                    }
                }
                else
                {
                    var roundCreateResult = await _roundService.CreateAsync(round);
                    if (roundCreateResult.IsError)
                    {
                        return roundCreateResult.Errors;
                    }
                }
            }
            await _workoutRepository.UpdateAsync(workout);
            await _unitOfWork.CommitChangesAsync();
            return workout;
        }
    }
}
