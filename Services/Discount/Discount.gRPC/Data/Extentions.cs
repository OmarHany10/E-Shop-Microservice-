using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class Extentions
    {
        public static async Task<IApplicationBuilder> UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            await db.Database.MigrateAsync();

            return app;
        }
    }
}
