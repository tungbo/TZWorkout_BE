using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Workouts;
using TunzWorkout.Application.Common.Services.Workouts;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class WorkoutController : ApiController
    {
        private readonly IWorkoutService _workoutService;
        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkoutAsync([FromBody] CreateWorkoutRequest request)
        {
            var toWorout = request.MapToWorkout();
            var result = await _workoutService.CreateAsync(toWorout);
            return result.Match(result => Ok(result.MapToResponse()), Problem);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWorkoutAsync(UpdateWorkoutRequest request, Guid id)
        {
            var toWorout = request.MapToWorkout(id);
            var result = await _workoutService.UpdateAsync(toWorout);
            return result.Match(result => Ok(result.MapToResponse()), Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkoutAsync()
        {
            var result = await _workoutService.GetAllAsync();
            return result.Match(result => Ok(result.MapToResponse()), Problem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWorkoutByIdAsync(Guid id)
        {
            var result = await _workoutService.GetByIdAsync(id);
            return result.Match(result => Ok(result.MapToResponse()), Problem);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWorkoutAsync(Guid id)
        {
            var result = await _workoutService.DeleteAsync(id);
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
