using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions;
using Carter;
using FluentValidation;
using Marten;

namespace Catalog
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
            });
            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddMarten(opt =>
            {
                opt.Connection(builder.Configuration.GetConnectionString("cs"));
            }).UseLightweightSessions();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            var app = builder.Build();

            app.MapCarter();
            app.UseExceptionHandler(opt => { });

            app.Run();
        }
    }
}
