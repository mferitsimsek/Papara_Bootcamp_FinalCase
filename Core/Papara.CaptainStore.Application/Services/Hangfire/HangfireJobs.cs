using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Interfaces.Notification;

namespace Papara.CaptainStore.Application.Services.Hangfire
{
    public class HangfireJobs
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly INotificationService _notificationService;

        public HangfireJobs(IMessageConsumer messageConsumer, INotificationService notificationService)
        {
            _messageConsumer = messageConsumer;
            _notificationService = notificationService;
        }

        public void ManageRabbitMQConsumer()
        {
            _messageConsumer.StartConsuming(_notificationService);
        }
    }
}
