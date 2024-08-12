using StackExchange.Redis;

namespace Papara.CaptainStore.Application.Interfaces.Caching
{
    public interface IRedisService
    {
        IDatabase GetDatabase();
    }
}
