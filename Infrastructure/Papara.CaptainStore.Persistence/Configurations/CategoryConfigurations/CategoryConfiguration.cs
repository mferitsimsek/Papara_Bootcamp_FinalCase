using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Persistence.Configurations.CategoryConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(product => product.CategoryId);
            builder.HasIndex(product => product.CategoryName).IsUnique();
            builder.Property(product => product.CategoryName).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Url).IsRequired().HasMaxLength(250);
            builder.Property(product => product.Tag).IsRequired().HasMaxLength(3);          
            builder.Property(product => product.CreatedDate).IsRequired();
            builder.Property(product => product.CreatedUserId).IsRequired();
            builder.Property(product => product.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
