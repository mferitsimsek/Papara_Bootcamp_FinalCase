﻿namespace Papara.CaptainStore.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration);
        Task RemoveAsync(string key);
    }
}
