using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class OrdersFilterQueryHandler : IRequestHandler<OrdersFilterQueryRequest, ApiResponseDTO<List<OrderListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponseDTO<List<OrderListDTO>?>> Handle(OrdersFilterQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllByFilterAsync(order => order.PaymentCompleted == request.PaymentCompleted, [o => o.OrderDetails]);
                if (orders.Any())
                {
                    var orderListDTOs = _mapper.Map<List<OrderListDTO>>(orders);

                    return new ApiResponseDTO<List<OrderListDTO>?>(200, orderListDTOs, new List<string> { "Siparişler başarıyla getirildi." });
                }
                return new ApiResponseDTO<List<OrderListDTO>?>(200, null, new List<string> { "Listelenecek sipariş bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<OrderListDTO>?>(500, null, new List<string> { "Siparişleri getirme sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}