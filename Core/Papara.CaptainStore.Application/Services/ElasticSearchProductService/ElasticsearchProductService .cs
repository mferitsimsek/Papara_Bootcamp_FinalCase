using AutoMapper;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using Papara.CaptainStore.ElasticSearch;
using Papara.CaptainStore.ElasticSearch.Models;
using Serilog;

namespace Papara.CaptainStore.Application.Services.ElasticSearchProductService
{
    public class ElasticsearchProductService : IElasticsearchProductService
    {
        private readonly IElasticSearch _elasticSearch;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ElasticsearchProductService(IElasticSearch elasticSearch, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _elasticSearch = elasticSearch;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> IndexAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            int successCount = 0;
            int failCount = 0;

            foreach (var product in products)
            {
                var elasticModel = new ElasticSearchInsertUpdateModel(
                    elasticId: product.ProductId.ToString(),
                    indexName: "products",
                    item: product
                );

                var elasticResponse = await _elasticSearch.InsertAsync(elasticModel);

                if (elasticResponse.Success)
                {
                    successCount++;
                }
                else
                {
                    failCount++;
                    Log.Warning("Ürün Elasticsearch'e indexlenirken hata oluştu", elasticResponse.Message);
                }
            }

            return $"İndeksleme tamamlandı. Başarılı: {successCount}, Başarısız: {failCount}";
        }

        public async Task<List<ProductListDTO>> GetAllProductsAsync(int from = 0, int size = 100)
        {
            var searchParameters = new SearchParameters
            {
                IndexName = "products",
                From = from,
                Size = size
            };

            var elasticResults = await _elasticSearch.GetAllSearch<Product>(searchParameters);
            return _mapper.Map<List<ProductListDTO>>(elasticResults.Select(r => r.Item).ToList());
        }

        public async Task<List<ProductListDTO>> GetProductsByFieldNameAsync(string fieldName, string fieldValue, int from = 0, int size = 10)
        {
            var fieldParameters = new SearchByFieldParameters
            {
                IndexName = "products",
                From = from,
                Size = size,
                FieldName = fieldName,
                Value = fieldValue
            };

            var elasticResults = await _elasticSearch.GetSearchByField<Product>(fieldParameters);
            return _mapper.Map<List<ProductListDTO>>(elasticResults.Select(r => r.Item).ToList());
        }
    }
}