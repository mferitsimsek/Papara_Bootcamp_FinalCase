using Microsoft.Extensions.Caching.Distributed;
using Papara.CaptainStore.Application.Interfaces.Caching;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.Services.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public CacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                return JsonSerializer.Deserialize<T>(cachedData, jsonOptions);
            }
            return default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan slidingExpiration)
        {
            var options = new DistributedCacheEntryOptions()
               .SetAbsoluteExpiration(absoluteExpiration)
               .SetSlidingExpiration(slidingExpiration);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var serializedValue = JsonSerializer.Serialize(value, jsonOptions);
            await _distributedCache.SetStringAsync(key, serializedValue, options);
        }
        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public bool IsConnected()
        {
            return _connectionMultiplexer.IsConnected;
        }

    }
}
