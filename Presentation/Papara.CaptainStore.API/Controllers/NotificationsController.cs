using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.Notification;
using Papara.CaptainStore.Application.Services.Hangfire;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly HangfireJobs _hangfireJobs;

        public NotificationsController(IMessageProducer messageProducer,HangfireJobs hangfireJobs)
        {
            _messageProducer = messageProducer;
            _hangfireJobs = hangfireJobs;
        }

        [HttpPost("SetRecurringJob")]
        public string Recurring()
        {
            // HangfireJobs sınıfını kullanarak ManageRabbitMQConsumer metodunu 2 dakikaya zamanlıyoruz
            RecurringJob.AddOrUpdate("notificationjob", () => _hangfireJobs.ManageRabbitMQConsumer(), "*/2 * * * *");
            return "notificationjob";
        }

        [HttpPost("SendNotification")]
        public string SendNotification(NotificationTemplate template)
        {
            _messageProducer.ProduceMessage(template);
            return "1";
        }
    }
}

