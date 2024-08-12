using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.Interfaces.Message;
using Papara.CaptainStore.Application.Services.MailContentBuilder;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.DTOs.NotificationDTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Serilog;

namespace Papara.CaptainStore.Application.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailContentBuilder _emailContentBuilder;
        private readonly IMessageProducer _messageProducer;

        public PaymentService(UserManager<AppUser> userManager, IEmailContentBuilder emailContentBuilder, IMessageProducer messageProducer)
        {
            _userManager = userManager;
            _emailContentBuilder = emailContentBuilder;
            _messageProducer = messageProducer;
        }

        public bool IsValidCardNumber(string cardNumber)
        {
            // Basit bir Luhn algoritması implementasyonu
            int sum = 0;
            bool isEven = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';
                if (isEven)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }
                sum += digit;
                isEven = !isEven;
            }
            return sum % 10 == 0;
        }

        public bool IsValidExpirationDate(string expiryMonth, string expiryYear)
        {
            // ExpirationDate formatı MM/YYYY 
            int month = int.Parse(expiryMonth);
            int year = int.Parse(expiryYear);

            // Geçerlilik tarihi kontrolü
            if (month < 1 || month > 12 || year < DateTime.Now.Year)
            {
                return false;
            }

            if (year == DateTime.Now.Year && month < DateTime.Now.Month)
            {
                return false;
            }

            return true;
        }

        public string GetCardType(string cardNumber)
        {
            switch (cardNumber.Substring(0, 2))
            {
                case "34":
                case "37":
                    return "American Express";
                case "4":
                    return "Visa";
                case "51":
                case "52":
                case "53":
                case "54":
                case "55":
                    return "MasterCard";
                default:
                    return "Bilinmeyen Kart Tipi";
            }
        }

        public async Task SendPaymentCompletedEmailAsync(PaymentCompletedEmailDTO paymentDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(paymentDTO.CreatedUserId);
                if (user == null)
                {
                    throw new Exception($"Ödeme onay maili kullanıcı bulunamadığı için gönderilemedi. Kullanıcı ID:{paymentDTO.CreatedUserId}");
                }

                paymentDTO.CustomerEmail = user.Email;
                paymentDTO.CustomerName = $"{user.FirstName} {user.LastName}";

                var subject = $"Ödeme Tamamlandı - Sipariş No: {paymentDTO.OrderNumber}";
                var body = _emailContentBuilder.BuildPaymentConfirmationEmail(paymentDTO);
                var notificationTemplate = new NotificationTemplate
                {
                    Subject = subject,
                    Body = body,
                    RecipientEmail = paymentDTO.CustomerEmail
                };

                _messageProducer.ProduceMessage(notificationTemplate);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ödeme tamamlandı maili gönderilirken hata oluştu.");
            }
        }
    }
}
