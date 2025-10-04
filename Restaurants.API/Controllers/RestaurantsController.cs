using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController(IMediator mediator/*, IValidator validator*/) : ControllerBase
    {
        [HttpGet]
        //[AllowAnonymous] // if you want to allow anonymous access to this endpoint, use this attribute
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var rastaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(rastaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id: id));
            return Ok(restaurant);
        }


        [HttpPost]
        //public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        // by default sent json body will convert to the dto class. no need to explicitly add [FromBody]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
        {

            //using this condition we can validate the model also. but defualt is good enough
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //ValidationResult result = await validator.ValidateAsync((IValidationContext)command);
            //if (!result.IsValid)
            //{
            //    var problemDetails = new HttpValidationProblemDetails(result.ToDictionary())
            //    {
            //        Status = StatusCodes.Status400BadRequest,
            //        Title = "Incorrect details",
            //    };

            //    return BadRequest(problemDetails);
            //}
            //try
            //{
                int id = await mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            //}
            //catch (ValidationException e)
            //{
            //    return BadRequest(e.Errors);
            //}

            //return CreatedAtAction(nameof(GetById), new {id}, null);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
             await mediator.Send(new DeleteRestaurantCommand(id: id));

                return NoContent();
            
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            try
            {
                command.Id = id;

                //ValidationResult result = await validator.ValidateAsync((IValidationContext)command);
                //if (!result.IsValid)
                //{
                //    var problemDetails = new HttpValidationProblemDetails(result.ToDictionary())
                //    {
                //        Status = StatusCodes.Status400BadRequest,
                //        Title = "Incorrect details",
                //    };

                //    return BadRequest(problemDetails);
                //}

               await mediator.Send(command);

                    return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Errors);
            }

        }
    }
}
