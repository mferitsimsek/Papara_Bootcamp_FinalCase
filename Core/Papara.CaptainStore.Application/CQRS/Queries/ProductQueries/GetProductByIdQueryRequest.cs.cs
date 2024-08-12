using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.ProductQueries
{
    public class GetProductByIdQueryRequest : IRequest<ApiResponseDTO<ProductListDTO?>>
    {
        public int ProductId { get; set; }
    }
}
