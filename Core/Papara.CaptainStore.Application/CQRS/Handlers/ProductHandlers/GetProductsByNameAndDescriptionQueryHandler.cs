using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.ProductQueries;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using Papara.CaptainStore.ElasticSearch;
using Papara.CaptainStore.ElasticSearch.Models;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class GetProductsByNameAndDescriptionQueryHandler : IRequestHandler<GetProductsByNameAndDescriptionQueryRequest, ApiResponseDTO<List<ProductListDTO>?>>
    {
        private readonly IMapper _mapper;
        private readonly IElasticSearch _elasticSearch;
        public GetProductsByNameAndDescriptionQueryHandler(IMapper mapper, IElasticSearch elasticSearch)
        {
            _mapper = mapper;
            _elasticSearch = elasticSearch;
        }

        public async Task<ApiResponseDTO<List<ProductListDTO>?>> Handle(GetProductsByNameAndDescriptionQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var searchParameters = new SearchByQueryParameters
                {
                    IndexName = "products", 
                    From = 0,
                    Size = 10,
                    Query = request.SearchTerm,
                    QueryName = "productQuery",
                    Fields = new[] { "productName","description" }
                };

                var elasticResults = await _elasticSearch.GetSearchBySimpleQueryString<Product>(searchParameters);


                if (elasticResults != null && elasticResults.Any())
                {
                    var products = elasticResults.Select(r => r.Item).ToList();
                    var productsDto = _mapper.Map<List<ProductListDTO>>(products);
                    return new ApiResponseDTO<List<ProductListDTO>?>(200, productsDto, new List<string> { "Arama kriterlerinizle eşleşen ürünler başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<ProductListDTO>?>(404, null, new List<string> { "Arama kriterlerinizle eşleşen ürün bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<ProductListDTO>?>(500, null, new List<string> { "Ürün listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}