using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, ApiResponseDTO<OrderListDTO?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDTO<OrderListDTO?>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(request.OrderId, "OrderDetails");
                if (order == null)
                {
                    return new ApiResponseDTO<OrderListDTO?>(404, null, new List<string> { "Sipariş bulunamadı." });
                }

                var orderDto = _mapper.Map<OrderListDTO>(order);
                return new ApiResponseDTO<OrderListDTO?>(200, orderDto, new List<string> { "Sorgulama işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<OrderListDTO?>(500, null, new List<string> { "Sorgulama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
