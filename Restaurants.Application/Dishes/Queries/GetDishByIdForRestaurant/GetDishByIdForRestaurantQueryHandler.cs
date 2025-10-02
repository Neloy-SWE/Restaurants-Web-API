using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dish with id {DishId} for restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException($"Restaurant with id {request.RestaurantId} not found");

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId) 
                ?? throw new NotFoundException($"Dish with id {request.DishId} not found");

            var result = mapper.Map<DishDto>(dish);

            return result;
        }
    }
}
