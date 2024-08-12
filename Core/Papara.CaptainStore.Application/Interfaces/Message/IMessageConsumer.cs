using Papara.CaptainStore.Application.Interfaces.Notification;

namespace Papara.CaptainStore.Application.Interfaces.Message
{
    public interface IMessageConsumer
    {
        void StartConsuming(INotificationService notificationService);
        void StopConsuming();
    }
}
