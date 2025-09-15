using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rastaurants = await restaurantsService.GetAllRestaurants();
            return Ok(rastaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var restaurant = await restaurantsService.GetById(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

    }
}
