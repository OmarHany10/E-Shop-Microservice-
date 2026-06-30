using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using Carter;
using Marten;

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

            var app = builder.Build();

            app.MapCarter();
            app.UseExceptionHandler(opt => { });

            app.Run();
        }
    }
}
