using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    //[Route("[controller]")] // this will set defult route to /WeatherForecast (controller name).
    //[Route("api/[controller]")] // this will set api before to the controller name.
    [Route("api/weatherResult")] // custom route

    // initialize object useing dependency injection
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService) : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;

        //private readonly WeatherForecastService _weatherForecastService = new();
        private readonly WeatherForecastService _weatherForecastService = weatherForecastService;

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.Get();
        }
    }
}
