using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Goals;
using TunzWorkout.Application.Common.Services.Goals;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class GoalController : ApiController
    {
        private readonly IGoalService _goalService;
        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] CreateGoalRequest request)
        {
            var toGoal = request.MapToGoal();
            var result = await _goalService.CreateAsync(toGoal);
            return result.Match(goal => CreatedAtAction(nameof(GetGoalById), new { id = goal.Id }, goal.MapToResponse()), Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGoals()
        {
            var result = await _goalService.GetAllAsync();
            return result.Match(goals => Ok(goals.MapToResponse()), Problem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGoalById([FromRoute] Guid id)
        {
            var result = await _goalService.GetGoalByIdAsync(id);
            return result.Match(goal => Ok(goal.MapToResponse()), Problem);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGoal([FromBody] UpdateGoalRequest request, [FromRoute] Guid id)
        {
            var toGoal = request.MapToGoal(id);
            var result = await _goalService.UpdateAsync(toGoal);
            return result.Match(goal => Ok(goal.MapToResponse()), Problem);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGoal([FromRoute] Guid id)
        {
            var result = await _goalService.DeleteByIdAsync(id);
            return result.Match(deleted => NoContent(), Problem);
        }
    }
}
