using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }

        private ProductId(Guid Id)
        {
            Value = Id;
        }

        public static ProductId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);

            if (guid == Guid.Empty)
            {
                throw new DomainException("ProductId must be not null");
            }

            return new ProductId(guid);
        }

    }
}
