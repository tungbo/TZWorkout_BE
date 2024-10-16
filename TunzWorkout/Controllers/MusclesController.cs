using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Muscles;
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
        public async Task<IActionResult> CreateMuscle([FromForm]CreateMuscleRequest request)
        {
            if (request is null)
            {
                //return Problem(detail: "Muscle is null", statusCode: 300, title: "Error");
                return BadRequest("Invalid request: Muscle data is required.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var muscle = request.MapToMuscle();
            await _muscleService.CreateAsync(muscle);
            var response = muscle.MapToResponse();

            return CreatedAtAction(nameof(CreateMuscle), new {id = muscle.Id}, response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMuscles()
        {
            var muscles = await _muscleService.GetAllAsync();

            var response = new List<MuscleResponse>();

            foreach (var muscle in muscles)
            {
                response.Add(muscle.MapToResponse());
            }

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetMuscleById([FromRoute] Guid id)
        {
            var muscle = await _muscleService.MuscleByIdAsync(id);
            if(muscle is null)
            {
                return NotFound();
            }
            var response = muscle.MapToResponse();
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateMuscle([FromForm] UpdateMuscleRequest request, [FromRoute]Guid id)
        {
            var muscles = request.MapToMuscle(id);

            await _muscleService.UpdateAsync(muscles);

            var response = muscles.MapToResponse();
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveMuscle([FromRoute] Guid id)
        {
            var deleted = await _muscleService.DeleteByIdAsync(id);
            if(!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
