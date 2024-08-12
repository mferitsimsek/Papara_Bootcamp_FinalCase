using Serilog;
using System.Text.Json;

namespace Papara.CaptainStore.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // _next delegasyonunu çağır
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Hata loglama
                Log.Fatal(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={ex.Message}"
                );
                // Hata yanıtı oluşturma
                context.Response.StatusCode = 500;
                context.Request.ContentType = "application/json";
                var errorResponse = new { Message = "Internal Server Error" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
