using Microsoft.Extensions.Options;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces.Notification;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using System.Net;
using System.Net.Mail;

namespace Papara.CaptainStore.Application.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly SmtpOptions _smtpOptions;

        public NotificationService(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendEmailAsync(NotificationTemplate template)
        {
            var smtpClient = new SmtpClient(_smtpOptions.SmtpHost)
            {
                Port = _smtpOptions.SmtpPort,
                Credentials = new NetworkCredential(_smtpOptions.SmtpUser, _smtpOptions.SmtpPass),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpOptions.SmtpUser),
                Subject = template.Subject,
                Body = template.Body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(template.RecipientEmail);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
