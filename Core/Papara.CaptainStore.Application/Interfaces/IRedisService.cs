using StackExchange.Redis;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IRedisService
    {
        IDatabase GetDatabase();
    }
}
