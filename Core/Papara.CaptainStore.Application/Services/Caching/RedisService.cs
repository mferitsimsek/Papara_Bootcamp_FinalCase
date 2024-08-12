using Microsoft.Extensions.Options;
using Papara.CaptainStore.Application.Interfaces.CachingService;
using StackExchange.Redis;

namespace Papara.CaptainStore.Application.Services.Caching
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisService(IOptions<RedisConfiguration> redisConfiguration)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfiguration.Value.ConnectionString);
        }

        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.GetDatabase();
        }


        public class RedisConfiguration
        {
            public string ConnectionString { get; set; }
        }
    }
}
