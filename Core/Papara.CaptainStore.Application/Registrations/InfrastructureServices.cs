using Microsoft.Extensions.DependencyInjection;
using Papara.CaptainStore.Application.Interfaces.Caching;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.Caching;
using Papara.CaptainStore.Application.Services.Message;
using Papara.CaptainStore.Application.Services.Notification;
using Papara.CaptainStore.ElasticSearch;

namespace Papara.CaptainStore.Application.Registrations
{
    public static class InfrastructureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IMessageProducer, MessageProducer>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        }
    }
}
