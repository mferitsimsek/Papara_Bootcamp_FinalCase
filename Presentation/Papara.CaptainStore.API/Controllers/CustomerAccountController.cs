using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands;
using Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerAccounts()
        {
            var response = await _mediator.Send(new CustomerAccountListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerAccountById(int customerAccountId)
        {
            var response = await _mediator.Send(new CustomerAccountByIdQueryRequest { CustomerAccountId = customerAccountId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomerAccount(CustomerAccountUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> DepositBalanceCustomerAccount(CustomerAccountDepositCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePointsCustomerAccount(CustomerAccountPointsUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{CustomerAccountId}")]
        public async Task<IActionResult> DeleteCustomerAccount(int CustomerAccountId)
        {
            var response = await _mediator.Send(new CustomerAccountDeleteCommandRequest { CustomerAccountId = CustomerAccountId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
