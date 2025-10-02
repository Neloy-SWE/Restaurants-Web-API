using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException($"Restaurant with {request.RestaurantId} doesn't exist");

            await dishesRepository.Delete(restaurant.Dishes);
        }
    }
}
