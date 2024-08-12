using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Persistence.Configurations.AppRoleConfigurations
{
    public class AppRoleConfiguration
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.Name).IsUnique();
            builder.HasIndex(role => role.NormalizedName).IsUnique();
        }
    }
}
