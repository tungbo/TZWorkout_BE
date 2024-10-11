using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Images;

namespace TunzWorkout.Infrastructure.Data.Equipments
{
    public class EquipmentConfigurations : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.EquipmentImageId).IsRequired(false);

            builder.HasOne(eq => eq.Image).WithOne(i => i.Equipment).HasForeignKey<Image>(eq => eq.EquipmentImageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
