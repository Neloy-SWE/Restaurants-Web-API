using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    //[Route("[controller]")] // this will set defult route to /WeatherForecast (controller name).
    //[Route("api/[controller]")] // this will set api before to the controller name.
    [Route("api/weatherResult")] // custom route

    // initialize object useing dependency injection
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService) : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;

        //private readonly WeatherForecastService _weatherForecastService = new();
        private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;

        [HttpGet]
        [Route("list")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.Get();
        }

        [HttpGet("CurrentDay")]
        public WeatherForecast GetCurrentDayForecast()
        {
            return _weatherForecastService.Get().First();
        }

        [HttpGet("{take}/check")] // we can use the value of "take" only if we add parameter with the same name. 
        public void Check([FromQuery] int max, [FromRoute] int take)
        {
            Console.WriteLine("max: ", max, "take: ", take);
        }

        [HttpPost("CheckName")]
        public string CheckName([FromBody] string name)
        {
            return $"Hello {name}";
        }
    }
}
