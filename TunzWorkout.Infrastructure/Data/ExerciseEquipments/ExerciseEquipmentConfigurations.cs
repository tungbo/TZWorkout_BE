using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.ExerciseEquipments;

namespace TunzWorkout.Infrastructure.Data.ExerciseEquipments
{
    public class ExerciseEquipmentConfigurations : IEntityTypeConfiguration<ExerciseEquipment>
    {
        public void Configure(EntityTypeBuilder<ExerciseEquipment> builder)
        {
            builder.HasKey(ee => new { ee.ExerciseId, ee.EquipmentId });

            builder.HasOne(ee => ee.Exercise).WithMany(e => e.ExerciseEquipments).HasForeignKey(ee => ee.ExerciseId);
            builder.HasOne(ee => ee.Equipment).WithMany(eq => eq.ExerciseEquipments).HasForeignKey(ee => ee.EquipmentId);
        }
    }
}
