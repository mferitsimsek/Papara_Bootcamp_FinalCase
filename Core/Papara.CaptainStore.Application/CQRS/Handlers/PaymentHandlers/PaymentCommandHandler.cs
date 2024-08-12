using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services.PaymentServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.DTOs.PaymentDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.PaymentHandlers
{
    public class PaymentCommandHandler : IRequestHandler<PaymentCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IPaymentService _paymentService;
        private readonly ISessionContext _sessionContext;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentCommandHandler(IPaymentService paymentService, ISessionContext sessionContext, IUnitOfWork unitOfWork)
        {
            _paymentService = paymentService;
            _sessionContext = sessionContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<object?>> Handle(PaymentCommandRequest request, CancellationToken cancellationToken)
        {

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return new ApiResponseDTO<object?>(404, null, new List<string> { "Sipariş bulunamadı." });
            }

            if (!_paymentService.IsValidCardNumber(request.CardNumber))
            {
                return new ApiResponseDTO<object?>(400, null, new List<string> { "Geçersiz kart numarası." });
            }
            if (!_paymentService.IsValidExpirationDate(request.ExpiryMonth, request.ExpiryYear))
            {
                return new ApiResponseDTO<object?>(400, null, new List<string> { "Geçersiz son kullanma tarihi." });
            }
            await Task.Delay(1000, cancellationToken); // Ödeme işlemi simülasyonu

            string cardType = _paymentService.GetCardType(request.CardNumber);

            var result = new PaymentResponseDTO
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString(),
                CardType = cardType
            };
            var payment = new PaymentCompletedEmailDTO
            {
                OrderNumber = order.OrderNumber,
                TransactionId = Guid.Parse(result.TransactionId),
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = order.CreatedUserId.ToString(),
                PaidAmount = Math.Max(0, order.BasketTotal - order.CouponDiscountAmount - order.PointsTotal)
            };
            await _paymentService.SendPaymentCompletedEmailAsync(payment);
            return new ApiResponseDTO<object?>(201, result, new List<string> { "Ödeme başarıyla gerçekleşti." });
        }
    }
}
