using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Papara.CaptainStore.Application.CQRS.Queries.LocationQueries;
using Papara.CaptainStore.Application.Extensions;

namespace Papara.CaptainStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            var response = await _mediator.Send(new GetAllCountryQueryRequest());
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }

        [HttpGet("{countryId}")]
        public async Task<IActionResult> Cities(int countryId)
        {
            var response = await _mediator.Send(new GetAllCityByCountryIdQueryRequest { CountryId = countryId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> Districts(int cityId)
        {
            var response = await _mediator.Send(new GetAllDistrictByCityIdQueryRequest { CityId = cityId });
            return this.ReturnResponseForApiResponseDtoExtension(response);
        }
    }
}
