using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.ProductCommands
{
    public class ProductUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Features { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public int PointsEarnedPct { get; set; }
        public int MaxPoints { get; set; }
        public int CategoryId { get; set; }
    }
}
