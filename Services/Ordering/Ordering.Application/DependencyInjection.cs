using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
                conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}
