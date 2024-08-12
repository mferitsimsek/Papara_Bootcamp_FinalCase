using Microsoft.AspNetCore.Mvc;

namespace Papara.CaptainStore.Application.Extensions
{
    public static class ResponseForApiResponseDtoExtension
    {
        public static IActionResult ReturnResponseForApiResponseDtoExtension(this ControllerBase controller, dynamic dto)
        {
            if (dto.status.ToString().Contains("201"))
            {
                return controller.Created("", dto);
            }
            else if (dto.status.ToString().Contains("20"))
            {
                return controller.Ok(dto);
            }
            else if (dto.status.ToString().Contains("404"))
            {
                return controller.NotFound(dto);
            }
            else if (dto.status.ToString().Contains("30"))
            {
                return controller.BadRequest(dto);
            }
            else
            {
                return controller.Ok(dto);
            }
        }
    }
}
