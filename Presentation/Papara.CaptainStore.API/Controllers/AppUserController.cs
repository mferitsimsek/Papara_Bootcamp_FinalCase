using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.CQRS.Queries.AppUserQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppUsers()
        {
            var response = await _mediator.Send(new AppUserListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAppUser(AppUserLoginQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppUser(AppUserCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdminUser(AdminUserCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAppUser(AppUserUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{appUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppUser(Guid appUserId)
        {
            var response = await _mediator.Send(new AppUserDeleteCommandRequest { AppUserId = appUserId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> AppUserPasswordReset(AppUserPasswordResetCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }

    }
}
