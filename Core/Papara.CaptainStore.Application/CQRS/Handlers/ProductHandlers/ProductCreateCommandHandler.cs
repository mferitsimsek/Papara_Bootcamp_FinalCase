using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Product> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly ProductService _productService;

        public ProductCreateCommandHandler(IMapper mapper, IValidator<Product> validator, IUnitOfWork unitOfWork, ISessionContext sessionContext, ProductService productService)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _productService = productService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kullanıcı kimliği alınıyor
                //Guid appUserId = Guid.Parse( _sessionContext.Session.UserId);

                //if (appUserId != null) request.CreatedUserId =appUserId;

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



                await _productService.SaveProduct(product);                

                return new ApiResponseDTO<object?>(201, _mapper.Map<ProductListDTO>(result.data), new List<string> { "Ürün Kayıt işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata işleme
                //return HandleException(ex);
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Ürün Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }

  
        private async Task<ICollection<Category>> GetCategoriesByIdsAsync(IEnumerable<int> categoryIds)
        {
            //var categories = new List<Category>();
            //foreach (var id in categoryIds)
            //{
            //    var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            //    if (category != null)
            //    {
            //        categories.Add(category);
            //    }
            //}

            //// Kategorileri ürüne atayın
            //product.Categories = categories;
            var tasks = categoryIds.Select(id => _unitOfWork.CategoryRepository.GetByIdAsync(id));
            var categories = await Task.WhenAll(tasks);
            return categories.Where(c => c != null).ToList();

            // ****************   EĞER KATEGORİ ID YANLIŞ GELİRSE HATA MESAJI   ******************************
        }
        //private IDTO<object?> HandleException(Exception ex)
        //{
        //    // Exception logging veya daha ileri işlem yapılabilir
        //    return new IDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu." });
        //}
    }
}
