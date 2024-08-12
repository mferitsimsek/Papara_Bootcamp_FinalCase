using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Application.CQRS.Queries.CouponQueries
{
    public class CouponsFilterQueryRequest : IRequest<ApiResponseDTO<List<CouponListDTO>?>>
    {
        public FilterType FilterType { get; set; }
    }
}
