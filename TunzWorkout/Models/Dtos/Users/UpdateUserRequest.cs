using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Api.Models.Dtos.Users
{
    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
