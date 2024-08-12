using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.PaymentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentCommandRequest request)
        {

            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);            
        }
    }
}
