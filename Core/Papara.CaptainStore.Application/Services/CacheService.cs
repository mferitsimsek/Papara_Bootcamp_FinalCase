using Microsoft.Extensions.Caching.Distributed;
using Papara.CaptainStore.Application.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
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
            return default(T);
        }

        public async Task SetAsync<T>(string key, T value,TimeSpan absoluteExpiration,TimeSpan slidingExpiration)
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
    }
}
