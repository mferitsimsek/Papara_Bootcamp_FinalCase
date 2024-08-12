using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Persistence.Configurations.LocationConfigurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.DistrictId);
            builder.Property(x => x.Definition).IsRequired();
            builder.Property(x => x.CityId).IsRequired();
        }
    }
}
