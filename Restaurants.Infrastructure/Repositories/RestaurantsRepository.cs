using Restaurants.Infrastructure.Persistence;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }
    }
}
