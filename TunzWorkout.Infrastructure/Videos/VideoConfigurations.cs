using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Exercises;
using TunzWorkout.Domain.Videos;

namespace TunzWorkout.Infrastructure.Videos
{
    public class VideoConfigurations : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.VideoPath).IsRequired();
            builder.Property(x => x.UploadDate).IsRequired();

            builder.HasOne(v => v.Exercise).WithOne(e => e.Video).HasForeignKey<Exercise>(e => e.VideoId);
        }
    }
}
