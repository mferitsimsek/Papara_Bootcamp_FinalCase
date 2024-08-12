using StackExchange.Redis;

namespace Papara.CaptainStore.Application.Interfaces.CachingService
{
    public interface IRedisService
    {
        IDatabase GetDatabase();
    }
}
