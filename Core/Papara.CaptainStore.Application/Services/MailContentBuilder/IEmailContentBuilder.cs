using Papara.CaptainStore.Domain.DTOs.MailDTOs;

namespace Papara.CaptainStore.Application.Services.MailContentBuilder
{
    public interface IEmailContentBuilder
    {
        string BuildOrderReceivedEmail(OrderReceivedEmailDTO orderReceivedEmailDTO);
        string BuildAccountCreatedEmail(AccountCreatedEmailDTO accountCreatedEmailDTO);
        string BuildPaymentConfirmationEmail(PaymentCompletedEmailDTO paymentConfirmationEmailDTO);
        string BuildSendCouponEmail(CouponSendEmailDTO couponSendEmailDTO);
    }
}
