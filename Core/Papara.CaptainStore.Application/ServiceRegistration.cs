using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Mappings;
using Papara.CaptainStore.Application.Registrations;
using Papara.CaptainStore.Application.Services;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
            .AddXmlDataContractSerializerFormatters();

            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAuthenticationServices();
            services.AddValidationServices();
            services.AddBusinessServices();
            services.AddInfrastructureServices();

            services.AddHttpClient<IHttpClientService, HttpClientService>();

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new AppRoleProfile());
                opt.AddProfile(new AppUserProfile());
                opt.AddProfile(new LocationProfile());
                opt.AddProfile(new ProductProfile());
                opt.AddProfile(new CategoryProfile());
                opt.AddProfile(new CustomerAccountProfile());
                opt.AddProfile(new CouponProfile());
                opt.AddProfile(new OrderProfile());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = " Captain Store Management", Version = "v1.0" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Captain Store for IT Company",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });
            });


        }
    }
}
