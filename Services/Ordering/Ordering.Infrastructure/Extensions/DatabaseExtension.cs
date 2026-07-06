using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Extensions
{
    public static class DatabaseExtension
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(AppDbContext context)
        {
            await SeedCustomerData(context);
            await SeedProductData(context);
            await SeedOrderData(context);
        }

        private static async Task SeedCustomerData(AppDbContext context)
        {
            if(!context.Customers.Any())
            {
                context.Customers.AddRange(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }


        private static async Task SeedProductData(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedOrderData(AppDbContext context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
