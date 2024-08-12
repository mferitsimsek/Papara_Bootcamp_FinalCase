using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.ProductQueries
{
    public class GetProductsByCategoryIdQueryRequest : IRequest<ApiResponseDTO<List<ProductListDTO?>>>
    {
        public int CategoryId { get; set; }
    }
}
