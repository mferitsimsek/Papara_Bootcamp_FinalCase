using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Persistence.Configurations.LocationConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.CountryId);
            builder.Property(x => x.Rewrite).IsRequired();
            builder.Property(x => x.Definition).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.HasMany(x => x.Cities).WithOne(x => x.Country).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
