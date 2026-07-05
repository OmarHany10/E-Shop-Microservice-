using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid Id)
        {
            Value = Id;
        }

        public static CustomerId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);

            if(guid == Guid.Empty)
            {
                throw new DomainException("CustomerId must be not null");
            }

            return new CustomerId(guid);
        }
    }
}
