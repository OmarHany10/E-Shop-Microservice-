using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddApiServices()
                .AddApplicationService()
                .AddInfrastructureService(builder.Configuration);

            var app = builder.Build();

            app.UseApiServices();

            app.Run();
        }
    }
}
