using Serilog;
using System.Net;
using System.Text.Json;

namespace Papara.CaptainStore.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ApplicationException e:
                        // uygulama hataları
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found hataları
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // bilinmeyen diğer hatalar
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = GetErrorMessage(error) });

                Log.Error(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={error.Message} || " +
                    $"StackTrace={error.StackTrace}"
                );

                await response.WriteAsync(result);
            }
        }

        private string GetErrorMessage(Exception error)
        {
            if (_env.IsDevelopment())
            {
                return error.Message;
            }
            return "An unexpected error occurred.";
        }
    }
}
