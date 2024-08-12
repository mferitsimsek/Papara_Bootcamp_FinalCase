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

            // Burada gerçek ödeme işlemi simüle ediliyor
            await Task.Delay(1000, cancellationToken); // Ödeme işlemi simülasyonu

            var result = new PaymentResponseDTO
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString()
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
    }
}
