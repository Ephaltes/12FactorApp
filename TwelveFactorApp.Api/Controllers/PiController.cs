using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace TwelveFactorApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PiController : ControllerBase
{
    private readonly ILogger<PiController> _logger;
    private readonly IPiCalculator _piCalculator;
    private readonly IRedisDatabase _redisDatabase;

    public PiController(ILogger<PiController> logger, IPiCalculator piCalculator, IRedisDatabase redisDatabase)
    {
        _logger = logger;
        _piCalculator = piCalculator;
        _redisDatabase = redisDatabase;
    }

    [HttpGet]
    public string Get(int precision = 2)
    {
        _logger.LogInformation($"Requested Pi with {precision} precision");
        IDatabase database = _redisDatabase.GetRedisDb();
        
        RedisKey key = new RedisKey(precision.ToString());
        string? pi = database.StringGet(key);
        
        if (string.IsNullOrEmpty(pi))
        {
            pi = _piCalculator.GetPi(precision);
            database.StringSet(key, new RedisValue(pi));
            _logger.LogInformation($"Writing Pi with {precision} precision into Cache {pi}");
        }
        _logger.LogInformation($"Requested Pi with {precision} precision equals\n {pi}");
        return pi;
    }
}