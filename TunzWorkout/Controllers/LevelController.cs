using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Levels;
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
            var level = request.MapToLevel();
            await _levelService.CreateAsync(level);
            var response = level.MapToResponse();
            return CreatedAtAction(nameof(CreateLevel), new {id = level.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLevels()
        {
            var levels = await _levelService.GetAllAsync();
            var response = new List<LevelResponse>();

            foreach (var level in levels)
            {
                response.Add(level.MapToResponse());
            }
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetLevelById([FromRoute] Guid id)
        {
            var level = await _levelService.LevelByIdAsync(id);
            if(level is null)
            {
                return NotFound();
            }
            var response = level.MapToResponse();
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLevel([FromBody] UpdateLevelRequest request, [FromRoute] Guid id)
        {
            var level = request.MapToLevel(id);
            await _levelService.UpdateAsync(level);
            var response = level.MapToResponse();
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveLevel([FromRoute] Guid id)
        {
            var deleted = await _levelService.DeleteByIdAsync(id);
            if(!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
