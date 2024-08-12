using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.ProductQueries
{
    public class GetProductsByNameAndDescriptionQueryRequest : IRequest<ApiResponseDTO<List<ProductListDTO>?>>
    {
        public required string SearchTerm { get; set; } // Arama terimi product name içerisinden yada description da bulunması istenen herhangi bir tanım.
    }
}
