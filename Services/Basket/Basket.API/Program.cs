using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using Carter;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var assembly = typeof(Program).Assembly;

            builder.Services.AddCarter();
            builder.Services.AddMediatR(con =>
            {
                con.RegisterServicesFromAssemblies(assembly);
                con.AddOpenBehavior(typeof(ValidationBehavior<,>));
                con.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddMarten(opt =>
            {
                opt.Connection(builder.Configuration.GetConnectionString("cs"));
            }).UseLightweightSessions();

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.Decorate<IBasketRepository, CashedBasketRepository>();

            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = builder.Configuration.GetConnectionString("cashe");
                opt.InstanceName = "Basket";
            });

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("cs"))
                .AddRedis(builder.Configuration.GetConnectionString("cashe"));

            var app = builder.Build();

            app.MapCarter();

            app.UseExceptionHandler(opt => { });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}
