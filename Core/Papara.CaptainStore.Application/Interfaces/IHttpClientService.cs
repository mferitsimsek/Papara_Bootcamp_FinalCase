namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> SendRequestAsync<T>(HttpMethod method, string url, object? content = null) where T : class;
    }
}
