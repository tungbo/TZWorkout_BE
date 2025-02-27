using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TunzWorkout.Domain.Entities.Wishlists;

namespace TunzWorkout.Infrastructure.Data.Wishlists
{
    public class WishlistConfigurations : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasOne(x => x.User).WithMany(u => u.Wishlists).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Workout).WithMany(w => w.Wishlists).HasForeignKey(x => x.WorkoutId);
        }
    }
}
