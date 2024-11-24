using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Goals;

namespace TunzWorkout.Infrastructure.Data.Goals
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedNever();
            builder.Property(g => g.Name).IsRequired();

            builder.HasMany(g => g.Workouts).WithOne(w => w.Goal).HasForeignKey(w => w.GoalId);
        }
    }
}
