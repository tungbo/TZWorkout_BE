using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Users;
using TunzWorkout.Application.Common.Services.Users;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        //[Authorize(Policy = nameof(UserRole.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllAsync();
            return result.Match(users => Ok(users.MapToResponse()), Problem);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return result.Match(user => Ok(user.MapToResponse()), Problem);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest request, [FromRoute] Guid id)
        {
            var toUser = request.MapToUser(id);
            var result = await _userService.UpdateAsync(toUser);
            return result.Match(user => Ok(user.MapToResponse()), Problem);
        }
    }
}
