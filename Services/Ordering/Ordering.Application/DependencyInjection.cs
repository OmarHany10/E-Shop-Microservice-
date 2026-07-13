using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
                conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

            services.AddFeatureManagement();

            return services;
        }
    }
}
