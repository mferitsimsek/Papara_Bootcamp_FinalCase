using Papara.CaptainStore.Application.Services.Notification;

namespace Papara.CaptainStore.Application.Interfaces.Message
{
    public interface IMessageConsumer
    {
        void StartConsuming(INotificationService notificationService);
        void StopConsuming();
    }
}
