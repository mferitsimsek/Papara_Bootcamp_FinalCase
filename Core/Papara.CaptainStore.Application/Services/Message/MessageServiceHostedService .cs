using Microsoft.Extensions.Hosting;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.Notification;

namespace Papara.CaptainStore.Application.Services.Message
{
    public class MessageServiceHostedService : IHostedService
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly INotificationService _notificationService;

        public MessageServiceHostedService(IMessageConsumer messageConsumer, INotificationService notificationService)
        {
            _messageConsumer = messageConsumer;
            _notificationService = notificationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageConsumer.StartConsuming(_notificationService);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageConsumer.StopConsuming();
            return Task.CompletedTask;
        }
    }
}
