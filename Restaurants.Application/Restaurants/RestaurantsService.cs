using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            //var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity); // manual mapping
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants); // using AutoMapper

            return restaurantsDtos!;
        }

        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Getting restaurant {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);

            //var restaurantDto = RestaurantDto.FromEntity(restaurant); // manual mapping
            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant); // using AutoMapper

            return restaurantDto;
        }

        public async Task<int> Create(CreateRestaurantDto dto)
        {
            logger.LogInformation("Creating new restaurant");
            var restaurant = mapper.Map<Domain.Entities.Restaurant>(dto); // using AutoMapper
            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
