using System.ComponentModel.DataAnnotations;
using TunzWorkout.Domain.Enums;

namespace TunzWorkout.Api.Models.Dtos.Authentication
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid LevelId { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        public IEnumerable<Guid> SelectedGoalIds { get; set; }
    }
}
