using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Persistence.Configurations.AppUserConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Email).IsUnique();
            builder.HasIndex(user => user.PhoneNumber).IsUnique();
            builder.HasIndex(user => user.UserName).IsUnique();
        }
    }
}
