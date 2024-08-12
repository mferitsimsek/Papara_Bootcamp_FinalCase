using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Validators.AppUserValidators;

namespace Papara.CaptainStore.Application.Registrations
{
    public static class ValidationServices
    {
        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation() 
                    .AddFluentValidationClientsideAdapters(); // doğrulama kuralları sadece sunucu tarafında değil istemci tarafındada uygulanır. Örn: JavaScript ile 
            services.AddValidatorsFromAssemblyContaining<AppUserValidator>();
        }
    }
}
