using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.CQRS.Commands.ProductCommands
{
    public class ProductCreateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Features { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public int PointsPercentage { get; set; }
        public int MaxPoints { get; set; }
        public List<int> CategoryIds { get; set; }
        [JsonIgnore]
        public Guid CreatedUserId { get; set; }
    }
}
