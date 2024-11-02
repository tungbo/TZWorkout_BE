using Microsoft.EntityFrameworkCore;
using TunzWorkout.Application.Common.Interfaces;
using TunzWorkout.Domain.Entities.EquipmentImages;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.ExerciseEquipments;
using TunzWorkout.Domain.Entities.ExerciseMuscles;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.Images;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.MuscleImages;
using TunzWorkout.Domain.Entities.Muscles;
using TunzWorkout.Domain.Entities.Videos;
using TunzWorkout.Infrastructure.Data.ExerciseEquipments;
using TunzWorkout.Infrastructure.Data.ExerciseMuscles;

namespace TunzWorkout.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ExerciseEquipment> ExerciseEquipments { get; set; }
        public DbSet<ExerciseMuscle> ExerciseMuscles { get; set; }
        public DbSet<MuscleImage> MuscleImages { get; set; }
        public DbSet<EquipmentImage> EquipmentImages { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseEquipmentConfigurations());
            modelBuilder.ApplyConfiguration(new ExerciseMuscleConfigurations());
            base.OnModelCreating(modelBuilder);
        }

        public async Task BeginTransactionAsync()
        {
            await base.Database.BeginTransactionAsync();
        }
    }
}
