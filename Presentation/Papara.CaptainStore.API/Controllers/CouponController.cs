using MediatR;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.CQRS.Queries.CouponQueries;
using Papara.CaptainStore.Application.Extensions;
using Serilog;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            Log.Warning("Uyarı Mesajı.");
            Log.Error("Hata mesajı aldık.", "Hata");
            var response = await _mediator.Send(new CouponsListQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetCouponById(int couponId)
        {
            var response = await _mediator.Send(new GetCouponByIdQueryRequest { CouponId = couponId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetCouponsByFilter([FromBody] CouponsFilterQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponCreateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(CouponUpdateCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
        [HttpDelete("{couponId}")]
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            var response = await _mediator.Send(new CouponDeleteCommandRequest { CouponId = couponId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
