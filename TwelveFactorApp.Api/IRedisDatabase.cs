using StackExchange.Redis;

namespace TwelveFactorApp.Api;

public interface IRedisDatabase
{
    public IDatabase GetRedisDb();
}