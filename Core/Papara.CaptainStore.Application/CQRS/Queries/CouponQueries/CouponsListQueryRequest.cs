﻿using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.CouponQueries
{
    public class CouponsListQueryRequest : IRequest<ApiResponseDTO<List<CouponListDTO>?>>
    {
    }
}
