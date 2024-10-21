using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Levels;
using TunzWorkout.Application.Common.Errors;
using TunzWorkout.Application.Common.Services.Levels;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;
        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLevel([FromBody] CreateLevelRequest request)
        {
            if (request is null)
            {
                //return Problem(detail: "Muscle is null", statusCode: 300, title: "Error");
                return BadRequest("Invalid request: Level data is required.");
            }
            var toLevel = request.MapToLevel();
            var result = await _levelService.CreateAsync(toLevel);
            return result.Match(
                level => CreatedAtAction(nameof(GetLevelById), new { id = level.Id}, level.MapToResponse()), errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLevels()
        {
            var result = await _levelService.GetAllAsync();

            return result.Match(success =>
            {
                var response = success.MapToResponse();
                return Ok(response);
            },
            errors => ErrorHandlingExtensions.HandleError(errors));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLevelById([FromRoute] Guid id)
        {
            var result = await _levelService.GetLevelByIdAsync(id);
            return result.Match(
                level => Ok(level.MapToResponse()),
                errors => ErrorHandlingExtensions.HandleError(errors)
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLevel([FromBody] UpdateLevelRequest request, [FromRoute] Guid id)
        {
            var toLevel = request.MapToLevel(id);
            var result = await _levelService.UpdateAsync(toLevel);
            return result.Match(level => Ok(level.MapToResponse()), errors => ErrorHandlingExtensions.HandleError(errors));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveLevel([FromRoute] Guid id)
        {
            var result = await _levelService.DeleteByIdAsync(id);
            return result.Match(_ => NoContent(), errors => ErrorHandlingExtensions.HandleError(errors));
        }
    }
}
