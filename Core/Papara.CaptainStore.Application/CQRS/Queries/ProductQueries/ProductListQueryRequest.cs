using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.ProductQueries
{
    public class ProductListQueryRequest : IRequest<ApiResponseDTO<List<ProductListDTO>?>>
    {
    }

}
