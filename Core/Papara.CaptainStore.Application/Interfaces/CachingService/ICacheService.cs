namespace Papara.CaptainStore.Application.Interfaces.CachingService
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
        Task RemoveAsync(string key);
        bool IsConnected();
    }
}
