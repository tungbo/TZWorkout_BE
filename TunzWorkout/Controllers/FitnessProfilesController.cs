using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.FitnessProfiles;
using TunzWorkout.Application.Common.Services.FitnessProfiles;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class FitnessProfilesController : ApiController
    {
        private readonly IFitnessProfileService _fitnessProfileService;
        public FitnessProfilesController(IFitnessProfileService fitnessProfileService)
        {
            _fitnessProfileService = fitnessProfileService;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetFitnessProfileByUserId(Guid userId)
        {
            var result = await _fitnessProfileService.GetFitnessProfileByUserIdAsync(userId);
            return result.Match(fitnessProfile => Ok(fitnessProfile.MapToResponse()), Problem);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFitnessProfiles()
        {
            var result = await _fitnessProfileService.GetAllAsync();
            return result.Match(fitnessProfiles => Ok(fitnessProfiles.MapToResponse()), Problem);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateFitnessProfileAsync([FromForm]UpdateFitnessProfileRequest request,[FromRoute] Guid id)
        {
            var toFitnessProfile = request.MapToFitnessProfile(id);
            var result = await _fitnessProfileService.UpdateAsync(toFitnessProfile);
            return result.Match(fitnessProfile => Ok(fitnessProfile.MapToResponse()), Problem);
        }
    }
}
