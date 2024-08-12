using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CouponCommands
{
    public class CouponSendCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public string CouponCode { get; set; }
        public string UserId { get; set; }
    }
}
