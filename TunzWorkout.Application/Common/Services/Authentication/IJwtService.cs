using System.Security.Claims;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Domain.Entities.Users;

namespace TunzWorkout.Application.Common.Services.Authentication
{
    public interface IJwtService
    {
        AuthenticationCommand CreateJwtToken(User user);
        ClaimsPrincipal GetPrincipalFromJwtToken(string? token);
    }
}
