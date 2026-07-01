using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountDbContext: DbContext
    {
        public DiscountDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(new List<Coupon>{
                new Coupon {Id=1, ProductName="IPhone 15 pro", Description="IPhone 15 pro Desc", Amount=200 },
                new Coupon {Id=2, ProductName="IPhone 15 pro max", Description="IPhone 15 pro max Desc", Amount=400 },
            });
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
