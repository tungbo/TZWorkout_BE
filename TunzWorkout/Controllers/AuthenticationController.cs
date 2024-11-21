using Microsoft.AspNetCore.Mvc;
using TunzWorkout.Api.Mapping;
using TunzWorkout.Api.Models.Dtos.Authentication;
using TunzWorkout.Application.Common.Services.Authentication;

namespace TunzWorkout.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var toRegisterCommand = request.MapToRegisterCommand();
            var result = await _authenticationService.RegisterAsync(toRegisterCommand);
            return result.Match(success => Ok(success.MapToAuthenticationResponse()), Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var toLoginCommand = request.MapToLoginCommand();
            var result = await _authenticationService.LoginAsync(toLoginCommand);
            return result.Match(success => Ok(success.MapToAuthenticationResponse()), Problem);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string email)
        {
            var result = await _authenticationService.LogoutAsync(email);
            return result.Match(_ => NoContent(), Problem);
        }

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModelRequest tokenModel)
        {
            var toTokenModelCommand = tokenModel.MapToTokenModelCommand();
            var result = await _authenticationService.GenerateNewAccessToken(toTokenModelCommand);
            return result.Match(success => Ok(success.MapToAuthenticationResponse()), Problem);
        }
    }
}
