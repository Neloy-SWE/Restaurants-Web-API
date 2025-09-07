using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{

    public class TemperatureRequest
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }


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

        //[HttpGet]
        //[Route("list")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return _weatherForecastService.Get();
        //}

        //[HttpGet("CurrentDay")]
        ////public ObjectResult GetCurrentDayForecast()
        //public IActionResult GetCurrentDayForecast()
        //{
        //    //return _weatherForecastService.Get().First();
        //    var result = _weatherForecastService.Get().First();

        //    // check the use of status code with ObjectResult
        //    //return StatusCode(400, result);

        //    // use something built in
        //    //return BadRequest(result);

        //    return Ok(result);
        //}

        //[HttpGet("{take}/check")] // we can use the value of "take" only if we add parameter with the same name. 
        //public void Check([FromQuery] int max, [FromRoute] int take)
        //{
        //    Console.WriteLine("max: ", max, "take: ", take);
        //}

        //[HttpPost("CheckName")]
        //public string CheckName([FromBody] string name)
        //{
        //    return $"Hello {name}";
        //}

        [HttpPost("generate")]
        public IActionResult Generate([FromQuery] int count, [FromBody] TemperatureRequest temperatureRequest)
        {
            if (count < 0 || temperatureRequest.Max < temperatureRequest.Min)
            {
                return BadRequest("Count has to be positive number, and max must be greater than the min value");
            }
            var result = _weatherForecastService.Get(count, temperatureRequest.Min, temperatureRequest.Max);
            return Ok(result);
        }
    }
}
