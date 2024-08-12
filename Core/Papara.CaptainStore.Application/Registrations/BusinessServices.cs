using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Application.Services.CategoryServices;
using Papara.CaptainStore.Application.Services.CouponServices;
using Papara.CaptainStore.Application.Services.CustomerAccountServices;
using Papara.CaptainStore.Application.Services.ElasticSearchProductService;
using Papara.CaptainStore.Application.Services.MailContentBuilder;
using Papara.CaptainStore.Application.Services.OrderServices;
using Papara.CaptainStore.Application.Services.PaymentServices;
using Papara.CaptainStore.Application.Services.ProductServices;
using Papara.CaptainStore.Application.Services.TokenServices;
using Papara.CaptainStore.Application.Services.UserServices;

namespace Papara.CaptainStore.Application.Registrations
{
    public static class BusinessServices
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IEmailContentBuilder, EmailContentBuilder>();
            services.AddScoped<IElasticsearchProductService, ElasticsearchProductService>();
        }
    }
}
