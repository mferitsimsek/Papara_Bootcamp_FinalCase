using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Persistence.Registrations;

namespace Papara.CaptainStore.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDatabaseServices(configuration);
            services.AddIdentityServices(configuration);
            services.AddCachingServices(configuration);
            services.AddRepositoryServices();

            services.AddScoped<ISessionContext>(provider =>
            SessionContextFactory.CreateSessionContext(provider.GetService<IHttpContextAccessor>()));

            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));
            services.Configure<SmtpOptions>(configuration.GetSection("SmtpOptions"));

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
        }
    }
}
