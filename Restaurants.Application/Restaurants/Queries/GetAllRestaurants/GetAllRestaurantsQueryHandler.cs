using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    internal class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("\n\n\nGetting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            //var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity); // manual mapping
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants); // using AutoMapper

            return restaurantsDtos!;
        }
    }
}
