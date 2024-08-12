using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.Application.Interfaces.Message
{
    public interface IMessageService : IDisposable
    {
        bool IsConsuming { get; }
        void ProduceMessage(NotificationTemplate template);
        void StartConsuming();
        void StopConsuming();
    }
}
