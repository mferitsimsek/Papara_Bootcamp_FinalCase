using Microsoft.IO;
using Papara.CaptainStore.Application.Helpers;
using Serilog;

namespace Papara.CaptainStore.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly Action<RequestProfilerModel> _requestResponseHandler;
        private const int ReadChunkBufferLength = 4096;
        public RequestLoggingMiddleware(RequestDelegate next, Action<RequestProfilerModel> requestResponseHandler)
        {
            _next = next;
            _requestResponseHandler = requestResponseHandler;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information("RequestLoggingMiddleware.Invoke başlatıldı.");

            var model = new RequestProfilerModel
            {
                RequestTime = new DateTimeOffset(),
                Context = context,
                Request = await FormatRequest(context)
            };

            var originalBodyStream = context.Response.Body;
            using (MemoryStream responseBody = _recyclableMemoryStreamManager.GetStream())
            {
                context.Response.Body = responseBody;
                try
                {
                    await _next(context);

                    // Response body'yi oku ve logla
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);

                    responseBody.Seek(0, SeekOrigin.Begin);
                    model.Response = await FormatResponse(context, responseBody);
                    model.ResponseTime = DateTimeOffset.Now;
                    _requestResponseHandler(model); // İstek ve yanıt bilgilerini işlemek için handler'ı çağır
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Bir hata oluştu.");
                    throw;
                }
            }
        }
        private async Task<string> FormatResponse(HttpContext context, Stream responseBody)
        {
            // Response bilgilerini formatla
            var request = context.Request;
            var response = context.Response;

            return $"Http Response Information: {Environment.NewLine}" +
                   $"Schema:{request.Scheme} {Environment.NewLine}" +
                   $"Host: {request.Host} {Environment.NewLine}" +
                   $"Path: {request.Path} {Environment.NewLine}" +
                   $"QueryString: {request.QueryString} {Environment.NewLine}" +
                   $"StatusCode: {response.StatusCode} {Environment.NewLine}" +
                   $"Response Body: {await ReadStreamInChunks(responseBody)}";
        }
        private async Task<string> FormatRequest(HttpContext context)
        {
            // Request bilgilerini formatla
            var request = context.Request;

            return $"Http Request Information: {Environment.NewLine}" +
                   $"Schema:{request.Scheme} {Environment.NewLine}" +
                   $"Host: {request.Host} {Environment.NewLine}" +
                   $"Path: {request.Path} {Environment.NewLine}" +
                   $"QueryString: {request.QueryString} {Environment.NewLine}" +
                   $"Request Body: {await GetRequestBody(request)}";
        }
        private async Task<string> GetRequestBody(HttpRequest request)
        {

            request.EnableBuffering(); // Request body'yi okumak için buffering'i etkinleştirir

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);
            request.Body.Seek(0, SeekOrigin.Begin);
            return await ReadStreamInChunks(requestStream);
        }
        private static async Task<string> ReadStreamInChunks(Stream stream)
        {
            // Stream'i chunk'lar halinde oku
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[ReadChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = await reader.ReadBlockAsync(readChunk, 0, ReadChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}
