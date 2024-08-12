using Newtonsoft.Json;
using Papara.CaptainStore.Application.Interfaces;
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

        public async Task<T> SendRequestAsync<T>(HttpMethod method, string url, object? content = null) where T : class
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
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
