using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Extensions;

namespace Ordering.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services
            //    .AddApiServices(builder.Configuration)
            //    .AddApplicationService(builder.Configuration)
            //    .AddInfrastructureService(builder.Configuration);



            //var app = builder.Build();

            //app.UseApiServices();

            //if (app.Environment.IsDevelopment())
            //{
            //    await app.InitializeDatabaseAsync();
            //}

            //app.Run();



            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services
                    .AddApiServices(builder.Configuration)
                    .AddApplicationService(builder.Configuration)
                    .AddInfrastructureService(builder.Configuration);

                var app = builder.Build();
                app.UseApiServices();

                if (app.Environment.IsDevelopment())
                {
                    await app.InitializeDatabaseAsync();
                }

                app.Run();
            }
            catch (Exception ex)
            {
                // السطر ده هيطبعلك المشكلة في سطر واحد واضح جداً في الـ Logs
                Console.WriteLine($"\n\n\n\n ====> THE MISSING SERVICE IS: {ex.Message} <==== \n\n\n\n");
                throw;
            }
        }
    }
}
