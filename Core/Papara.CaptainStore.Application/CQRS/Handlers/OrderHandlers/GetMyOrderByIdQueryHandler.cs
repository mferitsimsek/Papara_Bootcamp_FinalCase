using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class GetMyOrderByIdQueryHandler : IRequestHandler<GetMyOrderByIdQuery, ApiResponseDTO<OrderListDTO?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionContext _sessionContext;
        public GetMyOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionContext = sessionContext;
        }

        public async Task<ApiResponseDTO<OrderListDTO?>> Handle(GetMyOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid userId = !string.IsNullOrEmpty(_sessionContext.Session.UserId)
                                ? Guid.Parse(_sessionContext.Session.UserId)
                                : Guid.Empty;

                var order = await _unitOfWork.OrderRepository.GetByFilterAsync(order => order.Id == request.OrderId && order.CreatedUserId == userId, [order => order.OrderDetails]);
                if (order == null)
                {
                    return new ApiResponseDTO<OrderListDTO?>(404, null, new List<string> { "Sipariş bulunamadı." });
                }

                var orderDto = _mapper.Map<OrderListDTO>(order);
                return new ApiResponseDTO<OrderListDTO?>(200, orderDto, new List<string> { "Sorgulama işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<OrderListDTO?>(500, null, new List<string> { "Sorgulama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
