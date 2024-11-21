using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.FitnessProfiles;

namespace TunzWorkout.Infrastructure.Data.FitnessProfiles
{
    public class FitnessProfileConfigurations : IEntityTypeConfiguration<FitnessProfile>
    {
        public void Configure(EntityTypeBuilder<FitnessProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.UserId).IsRequired();
            builder.Ignore(x => x.SelectedGoalIds);


            builder.HasOne(x => x.User)
                .WithOne(u => u.FitnessProfile).HasForeignKey<FitnessProfile>(x => x.UserId);
            builder.HasOne(x => x.Level).WithMany(l => l.FitnessProfiles).HasForeignKey(x => x.LevelId);
        }
    }
}
