namespace TunzWorkout.Api.Models.Dtos.Authentication
{
    public class TokenModelRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
