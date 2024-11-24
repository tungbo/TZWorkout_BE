using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Rounds;

namespace TunzWorkout.Infrastructure.Data.Rounds
{
    public class RoundConfiguration : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();
            builder.Property(r => r.Name).IsRequired();

            builder.HasMany(r => r.RoundExercises).WithOne(re => re.Round).HasForeignKey(re => re.RoundId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Workout).WithMany(w => w.Rounds).HasForeignKey(r => r.WorkoutId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
