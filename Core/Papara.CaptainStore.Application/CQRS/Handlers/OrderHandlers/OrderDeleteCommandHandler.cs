using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderService _orderService;

        public OrderDeleteCommandHandler(IUnitOfWork unitOfWork, OrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(OrderDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, "OrderDetails");
                if (order == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek sipariş bulunamadı." });
                }
                foreach (var detail in order.OrderDetails)
                {
                    detail.IsDeleted = true;
                }
                order.IsDeleted = true;

                await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.Complete();

                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}