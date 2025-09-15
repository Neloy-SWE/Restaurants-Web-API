using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [

                new ()
                {
                    Name = "The Gourmet Kitchen",
                    Description = "A fine dining experience with a variety of international cuisines.",
                    Category = "Fine Dining",
                    HasDelivery = true,
                    ContactEmail = "contact@gk.com",
                    Address = new Address
                    {
                        City = "New York",
                        Street = "123 Culinary St",
                        PostalCode = "10001"
                    },

                    Dishes = [
                        new Dish
                        {
                            Name = "Truffle Pasta",
                            Description = "Pasta with a rich truffle sauce.",
                            Price = 25.99M
                        },
                        new Dish
                        {
                            Name = "Seared Scallops",
                            Description = "Fresh scallops seared to perfection.",
                            Price = 30.50M
                        }
                    ],

                }


                ];
            return restaurants;
        }

    }
}
