using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.Application.Interfaces.Notification
{
    public interface INotificationService
    {
        Task SendEmailAsync(NotificationTemplate notificationTemplate);
    }
}
