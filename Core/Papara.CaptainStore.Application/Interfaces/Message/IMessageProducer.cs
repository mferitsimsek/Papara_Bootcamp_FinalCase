using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.Application.Interfaces.Message
{
    public interface IMessageProducer
    {
        void ProduceMessage(NotificationTemplate template);
    }
}
