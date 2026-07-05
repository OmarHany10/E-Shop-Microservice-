using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }

        private OrderId(Guid Id)
        {
            Value = Id;
        }

        public static OrderId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);

            if (guid == Guid.Empty)
            {
                throw new DomainException("OrderId must be not null");
            }

            return new OrderId(guid);
        }
    }
}
