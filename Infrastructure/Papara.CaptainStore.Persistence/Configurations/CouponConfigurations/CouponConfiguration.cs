using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Persistence.Configurations.CouponConfigurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CouponCode).IsRequired().HasMaxLength(10);
            builder.HasIndex(x => x.CouponCode).IsUnique();
            builder.Property(x => x.UsedCount).IsRequired();
            builder.Property(x => x.DiscountType).IsRequired();
            builder.Property(x => x.DiscountAmount).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.CreatedUserId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
