using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Interfaces.Notification;
using Papara.CaptainStore.Application.Services.Hangfire;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageConsumer _messageConsumer;
        private readonly INotificationService _notificationService;
        private readonly HangfireJobs _hangfireJobs;

        public NotificationsController(
            IMessageProducer messageProducer,
            IMessageConsumer messageConsumer,
            INotificationService notificationService,
            HangfireJobs hangfireJobs)
        {
            _messageProducer = messageProducer;
            _messageConsumer = messageConsumer;
            _notificationService = notificationService;
            _hangfireJobs = hangfireJobs;
        }

        [HttpPost("SetRecurringJob")]
        public string Recurring()
        {
            // HangfireJobs sınıfını kullanarak ManageRabbitMQConsumer metodunu zamanlıyoruz
            RecurringJob.AddOrUpdate("notificationjob", () => _hangfireJobs.ManageRabbitMQConsumer(), "*/2 * * * *");
            return "notificationjob";
        }

        [HttpPost("SendNotification")]
        public string SendNotification(NotificationTemplate template)
        {
            // IMessageProducer kullanarak mesajı RabbitMQ'ya gönderiyoruz
            _messageProducer.ProduceMessage(template);
            return "1";
        }
    }
}

