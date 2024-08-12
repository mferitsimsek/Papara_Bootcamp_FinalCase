using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Application.CQRS.Queries.CategoryQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorys()
        {
            var response = await _mediator.Send(new CategoryListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CategoryCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _mediator.Send(new CategoryDeleteCommandRequest { CategoryId = categoryId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
