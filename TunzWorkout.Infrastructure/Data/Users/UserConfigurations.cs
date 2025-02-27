using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Users;

namespace TunzWorkout.Infrastructure.Data.Users
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.RefreshToken).IsRequired(false);
            builder.Property(x => x.RefreshTokenExpiry).IsRequired(false);

            builder.HasOne(x => x.FitnessProfile)
                .WithOne(fp => fp.User).HasForeignKey<FitnessProfile>(x => x.UserId);
            builder.HasMany(x => x.Wishlists)
                .WithOne(w => w.User).HasForeignKey(x => x.UserId);
        }
    }
}
