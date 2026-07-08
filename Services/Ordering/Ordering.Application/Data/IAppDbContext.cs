using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Data
{
    public interface IAppDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Domain.Models.Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
