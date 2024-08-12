using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Persistence.Repositories;

namespace Papara.CaptainStore.Persistence.Registrations
{
    public static class RepositoryServices
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }

}
