using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.EquipmentImages;

namespace TunzWorkout.Infrastructure.Data.EquipmentImages
{
    public class EquipmentImageConfigurations : IEntityTypeConfiguration<EquipmentImage>
    {
        public void Configure(EntityTypeBuilder<EquipmentImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();

            builder.HasOne(ei => ei.Equipment).WithMany(e => e.EquipmentImages).HasForeignKey(ei => ei.EquipmentId);
        }
    }
}
