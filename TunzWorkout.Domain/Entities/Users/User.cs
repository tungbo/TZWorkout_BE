using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Domain.Entities.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public FitnessProfile FitnessProfile { get; set; }

    }
}
