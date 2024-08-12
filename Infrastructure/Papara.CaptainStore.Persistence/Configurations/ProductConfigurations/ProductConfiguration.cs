using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Persistence.Configurations.ProductConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.ProductId);
            builder.HasIndex(product => product.ProductName).IsUnique();
            builder.Property(product => product.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Price).IsRequired();
            builder.Property(product => product.Quantity).IsRequired().HasDefaultValue(0);
            builder.Property(product => product.Features).IsRequired().HasMaxLength(250);
            builder.Property(product => product.Description).IsRequired().HasMaxLength(2048);
            builder.Property(product => product.PointsPercentage).IsRequired().HasMaxLength(3).HasDefaultValue(0);
            builder.Property(product => product.MaxPoints).IsRequired();
            builder.Property(product => product.CreatedDate).IsRequired();
            builder.Property(product => product.CreatedUserId).IsRequired();
            builder.Property(product => product.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(product => product.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
