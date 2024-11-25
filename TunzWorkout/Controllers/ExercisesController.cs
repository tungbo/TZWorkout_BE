using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Exercises;
using TunzWorkout.Application.Common.Services.Exercises;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExercisesController : ApiController
    {
        private readonly IExerciseService _exerciseService;
        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        [HttpPost]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> CreateExercise(CreateExerciseRequest request)
        {
            var toExcercise = request.MapToExercise();
            var result = await _exerciseService.CreateAsync(toExcercise);
            return result.Match(exercise => Ok(exercise.MapToResponse()), Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetAllExercisesRequest request)
        {
            var options = request.MapToOptions();
            var result = await _exerciseService.GetAllAsync(options);
            var exCount = await _exerciseService.CountAsync(request.Name);
            return result.Match(exercises => Ok(exercises.MapToResponse(request.Page, request.PageSize, exCount)), Problem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetExerciseById([FromRoute] Guid id)
        {
            var result = await _exerciseService.ExerciseByIdAsync(id);
            return result.Match(exercise => Ok(exercise.MapToResponse()), Problem);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveExercise([FromRoute]Guid id)
        {
            var result = await _exerciseService.DeleteByIdAsync(id);
            return result.Match(_=>NoContent(),Problem);
        }

        [HttpPut("{id:guid}")]
        [RequestSizeLimit(50 * 1024 * 1024)]
        public async Task<IActionResult> UpdateExercise([FromForm] UpdateExerciseRequest request, [FromRoute] Guid id)
        {
            var toExercise = request.MapToExercise(id);
            var result = await _exerciseService.UpdateAsync(toExercise);
            return result.Match(exercise => Ok(exercise.MapToResponse()), Problem);
        }
    }
}
