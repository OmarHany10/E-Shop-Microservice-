using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Models
{
    public class Product: Entity<ProductId>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public static Product Create(ProductId productId, string name, decimal price)
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new Product() { Id = productId, Name = name, Price = price };
        }
    }
}
