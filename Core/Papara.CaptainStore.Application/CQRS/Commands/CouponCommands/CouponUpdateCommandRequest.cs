using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Enums;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.CouponCommands
{
    public class CouponUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>, IHasUpdatedUser
    {
        public int CouponId { get; set; }
        public required string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int UsedCount { get; set; }
        public int MaxUsageCount { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }
    }
}
