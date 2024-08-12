using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Services.MailContentBuilder
{
    public class EmailContentBuilder : IEmailContentBuilder
    {
        public string BuildOrderReceivedEmail(OrderReceivedEmailDTO orderReceivedEmailDTO)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Sipariş Onayı</title>
            </head>
            <body>
                <h2>Sayın {orderReceivedEmailDTO.CustomerName},</h2>
                <p>{orderReceivedEmailDTO.OrderNumber} numaralı siparişiniz başarıyla alındı.</p>

                <h3>Sipariş Detayları:</h3>
                <ul>
                    <li>Sipariş Tarihi: {orderReceivedEmailDTO.CreatedDate:dd.MM.yyyy HH:mm:ss}</li>
                    <li>Toplam Tutar: {orderReceivedEmailDTO.BasketTotal:C2} TL</li>
                    <li>İndirim Tutarı: {orderReceivedEmailDTO.DiscountTotal:C2} TL</li>
                </ul>

                <h3>Sipariş Edilen Ürünler:</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Ürün Adı</th>
                            <th>Adet</th>
                            <th>Fiyat</th>
                        </tr>
                    </thead>
                    <tbody>
                        {string.Join("", orderReceivedEmailDTO.OrderDetailDTOs.Select(detail => $@"
                            <tr>
                                <td>{detail.ProductName}</td>
                                <td>{detail.Quantity}</td>
                                <td>{detail.Price * detail.Quantity:C2} TL</td>
                            </tr>
                        "))}
                    </tbody>
                </table>
            
                <p>Teşekkür ederiz.</p>
            </body>
            </html>
            ";
        }

        public string BuildAccountCreatedEmail(AccountCreatedEmailDTO accountCreatedEmailDTO)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Hoş Geldiniz!</title>
            </head>
            <body>
                <h2>Sayın Müşterimiz, {accountCreatedEmailDTO.AppUser.FirstName} {accountCreatedEmailDTO.AppUser.LastName}</h2>

                <p>Hesabınız başarıyla oluşturuldu. 
                   Hesap numaranız: <strong>{accountCreatedEmailDTO.AccountNumber}</strong></p>

                <p>Bizi tercih ettiğiniz için teşekkür ederiz.</p>
            </body>
            </html>
            ";
        }

        public string BuildPaymentConfirmationEmail(PaymentCompletedEmailDTO paymentConfirmationEmailDTO)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Ödeme Onayı</title>
            </head>
            <body>
                <h2>Sayın {paymentConfirmationEmailDTO.CustomerName},</h2>
                <p>{paymentConfirmationEmailDTO.OrderNumber} numaralı siparişiniz için ödemeniz başarıyla alındı.</p>

                <h3>Ödeme Detayları:</h3>
                <ul>
                    <li>Ödeme Tarihi: {paymentConfirmationEmailDTO.CreatedDate:dd.MM.yyyy HH:mm:ss}</li>
                    <li>İşlem ID: {paymentConfirmationEmailDTO.TransactionId}</li>
                    <li>Ödenen Tutar: {paymentConfirmationEmailDTO.PaidAmount:C2} TL</li>
                </ul>

                <p>Teşekkür ederiz.</p>
            </body>
            </html>
            ";
        }

        public string BuildSendCouponEmail(CouponSendEmailDTO couponSendEmailDTO)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Yeni Kuponunuz</title>
            </head>
            <body>
                <h2>Sayın {couponSendEmailDTO.CustomerName},</h2>
                <p>Size özel bir kuponumuz var!</p>

                <h3>Kupon Detayları:</h3>
                <ul>
                    <li>Kupon Kodu: <strong>{couponSendEmailDTO.CouponCode}</strong></li>
                    <li>Geçerlilik Tarihi: {couponSendEmailDTO.CouponValidityDate}</li>
                    <li>İndirim Türü: {(couponSendEmailDTO.DiscountType == Domain.Enums.DiscountType.Percentage? "Yüzdelik" : "Nakit")}</li>
                    <li>İndirim Oranı: {couponSendEmailDTO.DiscountAmount}</li>
                </ul>

                <p>Keyifli alışverişler dileriz!</p>
            </body>
            </html>
            ";
        }
    }
}
