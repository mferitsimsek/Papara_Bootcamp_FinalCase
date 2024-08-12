using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class GetMyOrdersQueryHandler : IRequestHandler<GetMyOrdersQueryRequest, ApiResponseDTO<List<OrderListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionContext _sessionContext;

        public GetMyOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionContext = sessionContext;
        }
        public async Task<ApiResponseDTO<List<OrderListDTO>?>> Handle(GetMyOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _sessionContext.Session.UserId;
                if (userId == null) return new ApiResponseDTO<List<OrderListDTO>?>(401, null, new List<string> { "Geçerli bir kullanıcı bulunamadı." });

                var orders = await _unitOfWork.OrderRepository.GetAllByFilterAsync(order => order.CreatedUserId == Guid.Parse(userId), [o => o.OrderDetails]);
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
