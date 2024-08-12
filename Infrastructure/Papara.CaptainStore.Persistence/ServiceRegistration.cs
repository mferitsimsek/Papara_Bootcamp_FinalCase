using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Persistence.Contexts;
using Papara.CaptainStore.Persistence.Repositories;
using StackExchange.Redis;
using Papara.CaptainStore.Application.Services;
using static Papara.CaptainStore.Application.Services.RedisService;

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
            services.AddScoped<IUnitOfWork, Papara.CaptainStore.Persistence.UnitOfWork.UnitOfWork>();
            services.AddIdentity<AppUser, AppRole>()
                   .AddEntityFrameworkStores<MSSqlContext>()
                   .AddDefaultTokenProviders(); // Token üretimi için eklendi.
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
            services.AddSingleton<IRedisService, RedisService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration["Redis:Host"]}:{Configuration["Redis:Port"]}";
                options.InstanceName = Configuration["Redis:InstanceName"];
            });

            // Caching hizmeti
            services.AddSingleton<ICacheService, CacheService>();


            //var redisConfig = new ConfigurationOptions();
            //redisConfig.DefaultDatabase = 0;
            //redisConfig.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
            //services.AddStackExchangeRedisCache(opt =>
            //{
            //    opt.ConfigurationOptions = redisConfig;
            //    opt.InstanceName = Configuration["Redis:InstanceName"];
            //});
        }
    }
}
