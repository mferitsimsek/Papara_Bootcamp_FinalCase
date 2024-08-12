using Papara.CaptainStore.Domain.DTOs.MailDTOs;

namespace Papara.CaptainStore.Application.Services.PaymentServices
{
    public interface IPaymentService
    {
        bool IsValidCardNumber(string cardNumber);
        bool IsValidExpirationDate(string expiryMonth, string expiryYear);
        string GetCardType(string cardNumber);
        Task SendPaymentCompletedEmailAsync(PaymentCompletedEmailDTO paymentCompletedEmailDTO);
    }
}
