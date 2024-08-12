using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Application.CQRS.Queries.ProductQueries;
using Papara.CaptainStore.Application.Extensions;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _mediator.Send(new ProductListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest { ProductId = productId});
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var response = await _mediator.Send(new GetProductsByCategoryIdQueryRequest { CategoryId = categoryId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var response = await _mediator.Send(new ProductDeleteCommandRequest { ProductId = productId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
