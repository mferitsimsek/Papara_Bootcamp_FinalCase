using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class OrdersListQueryHandler : IRequestHandler<OrdersListQueryRequest, ApiResponseDTO<List<OrderListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<List<OrderListDTO>?>> Handle(OrdersListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync([order => order.OrderDetails]);

                if (orders.Any())
                {
                    var ordersDto = _mapper.Map<List<OrderListDTO>>(orders);
                    return new ApiResponseDTO<List<OrderListDTO>?>(200, ordersDto, new List<string> { "Siparişler başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<OrderListDTO>?>(404, null, new List<string> { "Herhangi bir sipariş bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<OrderListDTO>?>(500, null, new List<string> { "Sipariş listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
