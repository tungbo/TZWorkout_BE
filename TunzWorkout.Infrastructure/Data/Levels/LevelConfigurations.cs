using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Levels;

namespace TunzWorkout.Infrastructure.Data.Levels
{
    public class LevelConfigurations : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description);

            builder.HasMany(l => l.Exercises).WithOne(e => e.Level).HasForeignKey(e => e.LevelId);

        }
    }
}
