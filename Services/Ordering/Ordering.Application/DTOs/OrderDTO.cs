using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.DTOs
{
    public record OrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderName { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public AddressDTO BillingAddress { get; set; }
        public PaymentDTO Payment { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public OrderStatus Status { get;  set; } = OrderStatus.Pending;

    }
}
