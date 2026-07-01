using Discount.gRPC.Data;
using Discount.gRPC.Services;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<DiscountDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("cs")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<DiscountServiec>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.UseMigration();

            app.Run();
        }
    }
}
