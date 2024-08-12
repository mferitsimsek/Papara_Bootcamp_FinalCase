using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Persistence.Contexts;

namespace Papara.CaptainStore.Persistence.Registrations
{
    public static class IdentityServices
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>()
                   .AddEntityFrameworkStores<MSSqlContext>()
                   .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
            });
        }
    }
}
