using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Mappings;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Application.Tools;
using Papara.CaptainStore.Application.Validators.AppUserValidators;
using Papara.CaptainStore.Domain.Consts;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddFluentValidationAutoValidation()
                      .AddFluentValidationClientsideAdapters(); // doğrulama kuralları sadece sunucu tarafında değil istemci tarafındada uygulanır. Örn: JavaScript ile 

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //{
            //    opt.RequireHttpsMetadata = false;
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidAudience = JwtTokenDefaults.ValidAudience,
            //        ValidIssuer = JwtTokenDefaults.ValidIssuer,
            //        ClockSkew = TimeSpan.Zero,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true
            //    };
            //});
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtTokenDefaults.ValidIssuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtTokenDefaults.Key)),
                    ValidAudience = JwtTokenDefaults.ValidAudience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
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
            services.AddHttpClient<IHttpClientService, HttpClientService>();
            services.AddValidatorsFromAssemblyContaining<AppUserValidator>(); //Startup bulunduğu Assembly içindeki Validatörleri otomatik bulup DI ' a ekler..
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
            services.AddScoped<FileUploader>(); // KALDIRILABİLİR.
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CouponService>();
            services.AddScoped<CustomerAccountService>();
            services.AddScoped<OrderService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped <IUserService, UserService> ();
            services.AddScoped <ITokenService, TokenService> ();

        }
    }
}
