using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Validators;
using Restaurants.Application.Users;


namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            //services.AddScoped<IRestaurantsService, RestaurantsService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
                cfg.AddProfile<DishesProfile>();
            }
            );

            //services.AddScoped<IValidator<CreateRestaurantDto>, CreateRestaurantDtoValidator>();
            services.AddValidatorsFromAssembly(applicationAssembly);


            //services.AddValidatorsFromAssemblyContaining<CreateRestaurantCommandValidator>();
            //services.AddValidatorsFromAssemblyContaining<UpdateRestaurantCommandValidator>();
            //services.AddFluentValidationAutoValidation(); // not working
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }
    }
}
