using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Models
{
    public class Order: Aggregate<OrderId>
    {
        private readonly List<OrderItem> orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => orderItems;

        public CustomerId CustomerId { get; private set; }
        public OrderName OrderName { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Payment Payment { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(o => o.Price * o.Quantity);
            private set;
        }

        public static Order Create(Guid orderId, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = OrderId.Of(orderId),
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new AddOrderEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new UpdateOrderEvent(this));

        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            orderItems.Add(new OrderItem(Id, productId, quantity, price));
        }

        public void Remove(ProductId productId)
        {
            var item = orderItems.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
                orderItems.Remove(item);
        }
    }
}
