using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Persistence.Configurations.LocationConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.CityId);
            builder.Property(x => x.Definition).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.HasMany(x => x.Districts).WithOne(x => x.City).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
