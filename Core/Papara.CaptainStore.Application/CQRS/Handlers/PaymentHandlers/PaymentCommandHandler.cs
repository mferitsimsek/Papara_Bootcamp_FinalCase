using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.PaymentDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.PaymentHandlers
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommandRequest, ApiResponseDTO<object?>>
    {
        public async Task<ApiResponseDTO<object?>> Handle(PaymentCommandRequest request, CancellationToken cancellationToken)
        {
            if (!IsValidCardNumber(request.CardNumber))
            {
                return new ApiResponseDTO<object?>(400, null, new List<string> { "Geçersiz kart numarası." });
            }
            if (!IsValidExpirationDate(request.ExpiryMonth, request.ExpiryYear))
            {
                return new ApiResponseDTO<object?>(400, null, new List<string> { "Geçersiz son kullanma tarihi." });
            }
            // Burada gerçek ödeme işlemi simüle ediliyor
            await Task.Delay(1000, cancellationToken); // Ödeme işlemi simülasyonu

            string cardType = GetCardType(request.CardNumber);

            var result = new PaymentResponseDTO
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString(),
                CardType = cardType
            };

            return new ApiResponseDTO<object?>(201, result, new List<string> { "Ödeme başarıyla gerçekleşti." });
        }
        private bool IsValidCardNumber(string cardNumber)
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
            return (sum % 10 == 0);
        }
        private bool IsValidExpirationDate(string expiryMonth, string expiryYear)
        {
            // ExpirationDate formatı MM/YY olmalı
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
        private string GetCardType(string cardNumber)
        {
            switch (cardNumber.Substring(0, 2)) // İlk iki haneyi al
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
    }
}
