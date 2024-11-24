using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.RoundExercises;

namespace TunzWorkout.Infrastructure.Data.RoundExercises
{
    public class RoundExerciseConfigurations : IEntityTypeConfiguration<RoundExercise>
    {
        public void Configure(EntityTypeBuilder<RoundExercise> builder)
        {
            builder.HasKey(re => re.Id);
            builder.Property(re => re.Id).ValueGeneratedNever();

            builder.HasOne(re => re.Round).WithMany(r => r.RoundExercises).HasForeignKey(re => re.RoundId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(re => re.Exercise).WithMany(e => e.RoundExercises).HasForeignKey(re => re.ExerciseId);
        }
    }
}
