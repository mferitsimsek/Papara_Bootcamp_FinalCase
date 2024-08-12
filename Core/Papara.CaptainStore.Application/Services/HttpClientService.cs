using Newtonsoft.Json;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using System.Text;

namespace Papara.CaptainStore.Application.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponseDTO<T>> SendRequestAsync<T>(HttpMethod method, string url, object? content = null) where T : class
        {
            var request = new HttpRequestMessage(method, url);

            if (content != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<T>(responseContent);
                return new ApiResponseDTO<T>(200, data, new List<string> { "İşlem başarılı" });
            }
            else
            {
                return new ApiResponseDTO<T>((int)response.StatusCode, default, new List<string> { responseContent });
            }
        }
    }
}
