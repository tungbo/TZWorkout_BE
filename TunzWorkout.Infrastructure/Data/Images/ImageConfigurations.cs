using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Images;

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
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
