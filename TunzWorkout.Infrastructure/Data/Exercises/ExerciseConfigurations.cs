using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Exercises;

namespace TunzWorkout.Infrastructure.Data.Exercises
{
    public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name);
            builder.Property(x => x.HasEquipment);
            builder.Property(x => x.LevelId);

            builder.HasOne(e => e.Level)
                   .WithMany(l => l.Exercises)
                   .HasForeignKey(e => e.LevelId);

            builder.HasMany(e => e.Videos).WithOne(v => v.Exercise).HasForeignKey(v => v.ExerciseId);
        }
    }
}
