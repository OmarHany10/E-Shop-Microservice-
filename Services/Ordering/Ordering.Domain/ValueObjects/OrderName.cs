using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; }

        private OrderName(string value)
        {
            Value = value;
        }

        public static OrderName Of(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            //ArgumentOutOfRangeException.ThrowIfNotEqual(name.Length, DefaultLength);

            return new OrderName(name);
        }

    }
}
