using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Papara.CaptainStore.Application.Extensions
{
    public static class ResponseForApiResponseDtoExtension
    {
        public static IActionResult ReturnResponseForApiResponseDtoExtension(this ControllerBase controller, dynamic dto)
        {
            if (dto == null)
                return controller.BadRequest("Response DTO cannot be null");

            return dto.status switch
            {
                200 => controller.Ok(dto),
                201 => controller.Created("", dto),
                204 => controller.NoContent(),
                400 => controller.BadRequest(dto),
                404 => controller.NotFound(dto),
                _ => controller.StatusCode(dto.status, dto) // Bilinmeyen durumlar için statuscode olarak döner.
            };           
        }
    }
}
