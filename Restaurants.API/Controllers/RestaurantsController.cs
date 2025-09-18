using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController(IRestaurantsService restaurantsService, IValidator<CreateRestaurantDto> validator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rastaurants = await restaurantsService.GetAllRestaurants();
            return Ok(rastaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await restaurantsService.GetById(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }


        [HttpPost]
        //public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        // by default sent json body will convert to the dto class. no need to explicitly add [FromBody]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {

            //using this condition we can validate the model also. but defualt is good enough
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            ValidationResult result = await validator.ValidateAsync(createRestaurantDto);
            if (!result.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(result.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Incorrect details",
                };

                return BadRequest(problemDetails);
            }

            int id = await restaurantsService.Create(createRestaurantDto);
            return CreatedAtAction(nameof(GetById), new {id}, null);
        }
    }
}
