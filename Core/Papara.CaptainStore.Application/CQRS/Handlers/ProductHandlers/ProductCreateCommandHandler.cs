using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Services.ProductServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using Papara.CaptainStore.ElasticSearch;
using Papara.CaptainStore.ElasticSearch.Models;
using Serilog;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Product> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly IProductService _productService;
        private readonly IElasticSearch _elasticSearch;

        public ProductCreateCommandHandler(IMapper mapper, IValidator<Product> validator, ISessionContext sessionContext, IProductService productService, IElasticSearch elasticSearch)
        {
            _mapper = mapper;
            _validator = validator;
            _sessionContext = sessionContext;
            _productService = productService;
            _elasticSearch = elasticSearch;
        }
        public async Task<ApiResponseDTO<object?>> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var existingProduct = await _productService.CheckProductIsExist(request.ProductName);
                if (existingProduct != null) return existingProduct;

                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapForCreateAsync(
                            request,
                            _mapper,
                            _validator,
                            () => Task.FromResult<Product>(new Product())
                        );

                if (result.status != 200)
                {
                    return result;
                }

                var product = result.data as Product;
                product.Categories = await _productService.GetCategoriesByIdsAsync(request.CategoryIds);

                var response = await _productService.SaveProduct(product);

                if (response.status == 201)
                {
                    var elasticModel = new ElasticSearchInsertUpdateModel(
                    elasticId: product.ProductId.ToString(),
                       indexName: "products",
                       item: product
                   );
                    var elasticResponse = await _elasticSearch.InsertAsync(elasticModel);

                    if (!elasticResponse.Success)
                    {
                        Log.Warning("Ürün veritabanına kaydedildi ancak Elasticserach indexlerken hata oluştu", elasticResponse.Message);
                    }
                }

                return new ApiResponseDTO<object?>(201, _mapper.Map<ProductListDTO>(result.data), new List<string> { "Ürün Kayıt işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Ürün Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
