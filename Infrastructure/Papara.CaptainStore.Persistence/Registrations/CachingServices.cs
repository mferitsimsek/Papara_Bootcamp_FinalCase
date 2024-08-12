using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Helpers;
using StackExchange.Redis;

namespace Papara.CaptainStore.Persistence.Registrations
{
    public static class CachingServices
    {
        public static void AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConfiguration>(configuration.GetSection("Redis"));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}";
                options.InstanceName = configuration["Redis:InstanceName"];
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConfig = configuration.GetSection("Redis").Get<RedisConfiguration>();

                var configOption = new ConfigurationOptions
                {
                    EndPoints = { { redisConfig.Host, int.Parse(redisConfig.Port) } },
                };

                return ConnectionMultiplexer.Connect(configOption);
            });
        }
    }
}
