using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Exercises;
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
            var excercise = request.MapToExercise();
            await _exerciseService.CreateAsync(excercise);
            var response = excercise.MapToResponse();
            return Ok(response);
        }
    }
}
