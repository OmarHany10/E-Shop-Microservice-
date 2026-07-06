using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasConversion(id => id.Value, id => OrderItemId.Of(id));

            builder.Property(o => o.Price).IsRequired();
            builder.Property(o => o.Quantity).IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(o => o.OrderId);


        }
    }
}
