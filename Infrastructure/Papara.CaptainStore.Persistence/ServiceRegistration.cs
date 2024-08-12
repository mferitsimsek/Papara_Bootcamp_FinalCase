using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services.Hangfire;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Persistence.Contexts;
using Papara.CaptainStore.Persistence.Repositories;
using StackExchange.Redis;

namespace Papara.CaptainStore.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<MSSqlContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Local"));
            });
            services.AddHttpContextAccessor();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
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
            services.AddScoped<ISessionContext>(provider =>
            {
                var context = provider.GetService<IHttpContextAccessor>();
                var sessionContext = new SessionContext();
                sessionContext.Session = JwtManager.GetSession(context.HttpContext);
                sessionContext.HttpContext = context.HttpContext;
                return sessionContext;
            });
            services.Configure<RedisConfiguration>(Configuration.GetSection("Redis"));

            // Redis hizmeti
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration["Redis:Host"]}:{Configuration["Redis:Port"]}";
                options.InstanceName = Configuration["Redis:InstanceName"];
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConfig = Configuration.GetSection("Redis").Get<RedisConfiguration>();

                var configuration = new ConfigurationOptions
                {
                    EndPoints = { { redisConfig.Host, int.Parse(redisConfig.Port) } },
                };

                return ConnectionMultiplexer.Connect(configuration);
            });



            services.Configure<RabbitMQOptions>(Configuration.GetSection("RabbitMQOptions"));
            services.Configure<SmtpOptions>(Configuration.GetSection("SmtpOptions"));

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            services.AddScoped<HangfireJobs>();

        }
    }
}
