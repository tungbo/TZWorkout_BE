using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.EquipmentImages;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Goals;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.MuscleImages;
using TunzWorkout.Domain.Entities.Muscles;
using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Domain.Entities.UserGoals;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Entities.Videos;
using TunzWorkout.Domain.Entities.Workouts;
using TunzWorkout.Infrastructure.Data.ExerciseEquipments;
using TunzWorkout.Infrastructure.Data.ExerciseMuscles;
using TunzWorkout.Infrastructure.Data.UserGoals;

namespace TunzWorkout.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FitnessProfile> FitnessProfiles { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<UserGoal> UserGoals { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ExerciseEquipment> ExerciseEquipments { get; set; }
        public DbSet<ExerciseMuscle> ExerciseMuscles { get; set; }
        public DbSet<MuscleImage> MuscleImages { get; set; }
        public DbSet<EquipmentImage> EquipmentImages { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundExercise> RoundExercises { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public async Task CommitChangesAsync()
        {
            /*var changes = */
            await base.SaveChangesAsync();
            //Console.WriteLine($"Number of records changed: {changes}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseEquipmentConfigurations());
            modelBuilder.ApplyConfiguration(new ExerciseMuscleConfigurations());
            modelBuilder.ApplyConfiguration(new UserGoalConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
