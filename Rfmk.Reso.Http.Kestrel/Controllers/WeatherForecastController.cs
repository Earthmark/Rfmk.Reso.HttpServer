using Microsoft.AspNetCore.Mvc;

namespace Rfmk.Reso.Http.Kestrel.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<string> Get()
    {
        return
        [
            "a", "b", "c"
        ];
    }
}