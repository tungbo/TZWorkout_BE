using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Equipments;

namespace TunzWorkout.Infrastructure.Data.Equipments
{
    public class EquipmentConfigurations : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ImageId);

            builder.HasOne(eq => eq.Image).WithOne(i => i.Equipment).HasForeignKey<Equipment>(eq => eq.ImageId);
        }
    }
}
