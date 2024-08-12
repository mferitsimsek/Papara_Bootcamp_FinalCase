using MediatR;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.ProductCommands
{
    public class ProductUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>, IHasUpdatedUser
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Features { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public int PointsEarnedPct { get; set; }
        public int MaxPoints { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public Guid UpdatedUserId { get; set; }
    }
}
