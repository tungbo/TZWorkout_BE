using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Muscles;
using TunzWorkout.Application.Common.Errors;
using TunzWorkout.Application.Common.Services.Muscles;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleService _muscleService;
        public MusclesController(IMuscleService muscleService)
        {
            _muscleService = muscleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMuscle([FromForm] CreateMuscleRequest request)
        {
            if (request is null)
            {
                //return Problem(detail: "Muscle is null", statusCode: 300, title: "Error");
                return BadRequest("Invalid request: Muscle data is required.");
            }
            var toMuscle = request.MapToMuscle();
            var result = await _muscleService.CreateAsync(toMuscle);

            return result.Match(
                muscle => CreatedAtAction(nameof(GetMuscleById), new { id = muscle.Id }, muscle.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMuscles()
        {
            var result = await _muscleService.GetAllAsync();


            return result.Match(
                muscles => Ok(muscles.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMuscleById([FromRoute] Guid id)
        {
            var result = await _muscleService.MuscleByIdAsync(id);
            return result.Match(
                muscle => Ok(muscle.MapToResponse()), 
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMuscle([FromForm] UpdateMuscleRequest request, [FromRoute] Guid id)
        {
            var toMuscle = request.MapToMuscle(id);
            var result = await _muscleService.UpdateAsync(toMuscle);
            return result.Match(
                muscle => Ok(muscle.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveMuscle([FromRoute] Guid id)
        {
            var result = await _muscleService.DeleteByIdAsync(id);
            return result.Match(
                _ => NoContent(),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }
    }
}
