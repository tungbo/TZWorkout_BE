using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Images;
using TunzWorkout.Domain.Entities.Muscles;

namespace TunzWorkout.Infrastructure.Data.Images
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();

            builder.HasOne(i => i.Muscle).WithOne(m => m.Image).HasForeignKey<Muscle>(m => m.MuscleImageId);
            builder.HasOne(i => i.Equipment).WithOne(e => e.Image).HasForeignKey<Equipment>(e => e.EquipmentImageId);
        }
    }
}
