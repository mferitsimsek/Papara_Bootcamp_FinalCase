using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Application.CQRS.Queries.CategoryQueries;
using Papara.CaptainStore.Application.Extensions;
using Serilog;

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
            Log.Warning("Uyarı Mesajı.");
            var response = await _mediator.Send(new CategoryListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _mediator.Send(new CategoryDeleteCommandRequest { CategoryId = categoryId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
