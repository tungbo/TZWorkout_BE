

namespace TunzWorkout.Application.Commands.Authentication
{
    public class AuthenticationCommand
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
