using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Host.UseSerilog((context, configuration) =>
            //configuration
            //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
            //.WriteTo.File("Logs/Restaurants-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            //.WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy hh:mm:ss tt} {newLine}{Level:u3}] |{SourceContext}| {newLine}{Message:lj}{NewLine}{Exception}{newLine}{newLine}{newLine}")
                configuration
                .ReadFrom.Configuration(context.Configuration)
            );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, Id = "bearerAuth"
                }
            },
            []
        }
    });

            });
        }
    }
}
