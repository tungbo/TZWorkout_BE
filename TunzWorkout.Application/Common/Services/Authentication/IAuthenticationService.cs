
using ErrorOr;
using TunzWorkout.Application.Commands.Authentication;

namespace TunzWorkout.Application.Common.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ErrorOr<AuthenticationCommand>> RegisterAsync(RegisterCommand registerCommand);
        Task<ErrorOr<AuthenticationCommand>> LoginAsync(LoginCommand loginCommand);
        Task<ErrorOr<Success>> LogoutAsync(string email);
        Task<ErrorOr<AuthenticationCommand>> GenerateNewAccessToken(TokenModelCommand tokenModelCommand);
    }
}
