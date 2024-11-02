using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.MuscleImages;

namespace TunzWorkout.Infrastructure.Data.MuscleImages
{
    public class MuscleImageConfigurations : IEntityTypeConfiguration<MuscleImage>
    {
        public void Configure(EntityTypeBuilder<MuscleImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();

            builder.HasOne(mi => mi.Muscle).WithMany(m => m.MuscleImages).HasForeignKey(mi => mi.MuscleId);
        }
    }
}
