using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.Application.Services.Notification
{
    public interface INotificationService
    {
        Task SendEmailAsync(NotificationTemplate notificationTemplate);
    }
}
