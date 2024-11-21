using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.UserGoals;

namespace TunzWorkout.Infrastructure.Data.UserGoals
{
    public class UserGoalConfigurations : IEntityTypeConfiguration<UserGoal>
    {
        public void Configure(EntityTypeBuilder<UserGoal> builder)
        {
            builder.HasKey(ug => new { ug.FitnessProfileId, ug.GoalId });

            builder.HasOne(ug => ug.FitnessProfile).WithMany(fp => fp.UserGoals)
                .HasForeignKey(ug => ug.FitnessProfileId);
            builder.HasOne(ug => ug.Goal).WithMany(g => g.UserGoals)
                .HasForeignKey(ug => ug.GoalId);
        }
    }
}
