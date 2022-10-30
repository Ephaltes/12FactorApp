using System.Net;
using StackExchange.Redis;

namespace TwelveFactorApp.Api;

public class RedisDatabase : IRedisDatabase
{
   private ILogger<RedisDatabase> _logger;
   private IDatabase _database;
   public RedisDatabase(ILogger<RedisDatabase> logger)
   {
      _logger = logger;

      string? user = Environment.GetEnvironmentVariable("RedisUser");
      string? password = Environment.GetEnvironmentVariable("RedisPassword");
      string? endpoint = Environment.GetEnvironmentVariable("RedisEndpoint");

      if (string.IsNullOrEmpty(user))
      {
         _logger.LogError($"RedisUser Environment Variable is not set");
         throw new NullReferenceException(user);
      }
      
      if (string.IsNullOrEmpty(password))
      {
         _logger.LogError($"RedisPassword Environment Variable is not set");
         throw new NullReferenceException(password);
      }
      
      if (string.IsNullOrEmpty(endpoint))
      {
         _logger.LogError($"RedisEndpoint Environment Variable is not set");
         throw new NullReferenceException(endpoint);
      }
      
      ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
         new ConfigurationOptions
         {
            User = user,
            Password = password,
            EndPoints =  { endpoint }
         });

      _database = redis.GetDatabase();
   }
   public IDatabase GetRedisDb()
   {
      return _database;
   }
}