using ErrorOr;
using FluentValidation;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Application.Common.Services.Rounds
{
    public class RoundService : IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly IRoundExerciseRepository _roundExerciseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Round> _roundValidator;
        private readonly IValidator<RoundExercise> _roundEValidator;
        public RoundService(IRoundRepository roundRepository, IRoundExerciseRepository roundExerciseRepository, IUnitOfWork unitOfWork, IValidator<Round> roundValidator, IValidator<RoundExercise> roundEValidator)
        {
            _roundRepository = roundRepository;
            _roundExerciseRepository = roundExerciseRepository;
            _unitOfWork = unitOfWork;
            _roundValidator = roundValidator;
            _roundEValidator = roundEValidator;
        }
        public async Task<ErrorOr<Round>> CreateAsync(Round round)
        {
            var roundValidationResult = await _roundValidator.ValidateAsync(round);
            if (!roundValidationResult.IsValid)
            {
                return roundValidationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            await _roundRepository.CreateAsync(round);
            foreach (var roundExercise in round.RoundExercises)
            {
                var roundExerciseValidationResult = await _roundEValidator.ValidateAsync(roundExercise);
                if (!roundExerciseValidationResult.IsValid)
                {
                    return roundExerciseValidationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
                }
                await _roundExerciseRepository.CreateAsync(roundExercise);
            }
            return round;
        }

        public Task<ErrorOr<Deleted>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<Deleted>> DeleteRoundExAsync(Guid id)
        {
            await _roundExerciseRepository.DeleteRoundExAsync(id);
            await _unitOfWork.CommitChangesAsync();
            return Result.Deleted;
        }

        public Task<ErrorOr<IEnumerable<Round>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Round?>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<Round>> UpdateAsync(Round round)
        {
            var roundValidationResult = _roundValidator.ValidateAsync(round);
            if (!roundValidationResult.Result.IsValid)
            {
                return roundValidationResult.Result.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
            }
            var existingRound = await _roundRepository.RoundByIdAsync(round.Id);
            if (existingRound is null)
            {
                return Error.NotFound(description: "Round not found");
            }
            var existingRoundExercise = await _roundExerciseRepository.GetAllByRoundId(round.Id);
            foreach (var roundExercise in round.RoundExercises)
            {
                if (existingRoundExercise.Any(er => er.Id == roundExercise.Id))
                {
                    var roundExerciseValidationResult = await _roundEValidator.ValidateAsync(roundExercise);
                    if (!roundExerciseValidationResult.IsValid)
                    {
                        return roundExerciseValidationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
                    }
                    await _roundExerciseRepository.UpdateAsync(roundExercise);
                }
                else
                {
                    var roundExerciseValidationResult = await _roundEValidator.ValidateAsync(roundExercise);
                    if (!roundExerciseValidationResult.IsValid)
                    {
                        return roundExerciseValidationResult.Errors.ConvertAll(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage));
                    }
                    roundExercise.Id = Guid.NewGuid();
                    await _roundExerciseRepository.CreateAsync(roundExercise);
                }
            }
            await _roundRepository.UpdateAsync(round);
            await _unitOfWork.CommitChangesAsync();
            return existingRound;
        }
    }
}
