using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.OrderService;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.PaymentDTOs;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IOrderService _orderService;
        private readonly string _paymentApiUrl;

        public PaymentController(IHttpClientService httpClientService, IConfiguration configuration, IOrderService orderService)
        {
            _httpClientService = httpClientService;
            _paymentApiUrl = configuration.GetValue<string>("PaymentApiUrl");
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentCommandRequest request)
        {
            request.Amount = await _orderService.CalculatePaymentAmountAsync(request.OrderId);
            if (request.Amount <= 0)
            {
                return BadRequest(new { Message = "Ödeme işlemi başarısız. Sipariş ödeme işlemi zaten gerçekleştirilmiş" });

            }
            var response = await _httpClientService.SendRequestAsync<ApiResponseDTO<PaymentResponseDTO>>(
                        HttpMethod.Post,
                        $"{_paymentApiUrl}/api/Payment/ProcessPayment",
                        request);

            if (!response.status.ToString().StartsWith("20"))
            {
                return StatusCode((int)response.status, response.messages);
            }
            await _orderService.UpdateOrderPaymentStatusAsync(request.OrderId, true);
            return Ok(response);
        }
    }
}
