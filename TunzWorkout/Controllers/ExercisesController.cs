using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Exercises;
using TunzWorkout.Application.Common.Errors;
using TunzWorkout.Application.Common.Services.Exercises;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateExercise(CreateExerciseRequest request)
        {
            var toExcercise = request.MapToExercise();
            var result = await _exerciseService.CreateAsync(toExcercise);
            return result.Match(exercise => Ok(exercise.MapToResponse()), errors => ErrorHandlingExtensions.HandleError(errors));
        }
    }
}
