using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Persistence.Configurations.CustomerAccountConfigurations
{
    public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.AccountNumber).IsUnique();
            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.Balance).IsRequired();
            builder.Property(x => x.Points).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.CreatedUserId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.AppUser).WithMany().HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }


}
