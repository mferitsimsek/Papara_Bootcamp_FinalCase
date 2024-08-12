using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CouponCommands
{
    public class CouponDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CouponId { get; set; }
    }
}
