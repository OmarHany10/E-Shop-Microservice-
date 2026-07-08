namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly;
            services.AddMediatR(con =>
            {
                con.RegisterServicesFromAssemblies(assembly);
            });
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            return app;
        }
    }
}
