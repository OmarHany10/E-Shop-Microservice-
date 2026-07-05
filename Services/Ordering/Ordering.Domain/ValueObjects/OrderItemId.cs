using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        private OrderItemId(Guid Id)
        {
            Value = Id;
        }

        public static OrderItemId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);

            if (guid == Guid.Empty)
            {
                throw new DomainException("OrderItemId must be not null");
            }

            return new OrderItemId(guid);
        }
    }
}
