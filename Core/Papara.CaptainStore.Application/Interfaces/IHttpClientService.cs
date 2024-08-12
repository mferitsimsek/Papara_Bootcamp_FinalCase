using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<ApiResponseDTO<T>> SendRequestAsync<T>(HttpMethod method, string url, object? content = null) where T : class;
    }
}
