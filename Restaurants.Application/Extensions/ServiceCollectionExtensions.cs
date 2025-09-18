using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using FluentValidation;


namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantsService, RestaurantsService>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
                cfg.AddProfile<DishesProfile>();
            }
            );

            //services.AddScoped<IValidator<CreateRestaurantDto>, CreateRestaurantDtoValidator>();
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly, includeInternalTypes: true);
        }
    }
}
