using Carter;

namespace Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCarter();
            builder.Services.AddMediatR(con =>
            {
                con.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });
            
            var app = builder.Build();

            app.MapCarter();

            app.Run();
        }
    }
}
