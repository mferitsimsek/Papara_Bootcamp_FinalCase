using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.OrderCommands
{
    public class OrderCreateCommandRequest : IRequest<ApiResponseDTO<object?>>,IHasCreatedUser
    {
        public required string CouponCode { get; set; }

        public List<OrderDetailDTO> OrderDetails { get; set; }
        [JsonIgnore]
        public Guid CreatedUserId { get; set; }        
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
