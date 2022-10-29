using Microsoft.AspNetCore.Mvc;

namespace TwelveFactorApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PiController : ControllerBase
{
    private readonly ILogger<PiController> _logger;
    private readonly IPiCalculator _piCalculator;

    public PiController(ILogger<PiController> logger, IPiCalculator piCalculator)
    {
        _logger = logger;
        _piCalculator = piCalculator;
    }

    [HttpGet]
    public string Get(int precision = 2)
    {
        _logger.LogInformation($"Requested Pi with {precision} precision");
        string pi = _piCalculator.GetPi(precision);
        _logger.LogInformation($"Requested Pi with {precision} precision equals\n {pi}");
        return pi;
    }
}