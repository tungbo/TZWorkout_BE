using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.ExerciseMuscles;

namespace TunzWorkout.Infrastructure.Data.ExerciseMuscles
{
    public class ExerciseMuscleConfigurations : IEntityTypeConfiguration<ExerciseMuscle>
    {
        public void Configure(EntityTypeBuilder<ExerciseMuscle> builder)
        {
            builder.HasKey(em => new { em.MuscleId, em.ExerciseId });

            builder.HasOne(em => em.Muscle).WithMany(m => m.ExerciseMuscles).HasForeignKey(em => em.MuscleId);
            builder.HasOne(em => em.Exercise).WithMany(e => e.ExerciseMuscles).HasForeignKey(em => em.ExerciseId);
        }
    }
}
