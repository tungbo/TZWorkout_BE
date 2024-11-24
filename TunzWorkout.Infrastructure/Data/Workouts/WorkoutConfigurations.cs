using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Infrastructure.Data.Workouts
{
    public class WorkoutConfigurations : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedNever();
            builder.Property(w => w.Name).IsRequired();
            
            builder.HasOne(w => w.Level).WithMany(l => l.Workouts).HasForeignKey(w => w.LevelId);
            builder.HasOne(w => w.Goal).WithMany(g => g.Workouts).HasForeignKey(w => w.GoalId);
            builder.HasMany(w => w.Rounds).WithOne(r => r.Workout).HasForeignKey(r => r.WorkoutId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
