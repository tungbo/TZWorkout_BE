using Microsoft.EntityFrameworkCore;
using TunzWorkout.Domain.Exercises;
using TunzWorkout.Domain.Levels;
using TunzWorkout.Domain.Videos;

namespace TunzWorkout.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
