using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Infrastructure.Data.Muscles
{
    public class MuscleConfigurations : IEntityTypeConfiguration<Muscle>
    {
        public void Configure(EntityTypeBuilder<Muscle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.MuscleImageId).IsRequired(false);
            builder.Ignore(x => x.ImageFile);

            builder.HasOne(m => m.Image).WithOne(i => i.Muscle).HasForeignKey<Muscle>(m => m.MuscleImageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
