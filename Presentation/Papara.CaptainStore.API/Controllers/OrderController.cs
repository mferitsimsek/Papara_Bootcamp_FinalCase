using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Application.CQRS.Queries.OrderQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _mediator.Send(new OrdersListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var response = await _mediator.Send(new GetMyOrdersQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetMyOrderById(int orderId)
        {
            var response = await _mediator.Send(new GetMyOrderByIdQuery { OrderId = orderId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var response = await _mediator.Send(new GetOrderByIdQueryRequest { OrderId = orderId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrdersByFilter(bool paymentCompleted)
        {
            var response = await _mediator.Send(new OrdersFilterQueryRequest { PaymentCompleted = paymentCompleted });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var response = await _mediator.Send(new OrderDeleteCommandRequest { OrderId = orderId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
