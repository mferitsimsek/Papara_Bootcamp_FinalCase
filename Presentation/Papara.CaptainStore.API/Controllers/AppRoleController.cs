using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands;
using Papara.CaptainStore.Application.CQRS.Queries.AppRoleQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppRoles()
        {
            var response = await _mediator.Send(new AppRoleListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppRole(AppRoleCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppRole(AppRoleUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{appRoleId}")]
        public async Task<IActionResult> DeleteAppRole(Guid appRoleId)
        {
            var response = await _mediator.Send(new AppRoleDeleteCommandRequest { AppRoleId = appRoleId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
