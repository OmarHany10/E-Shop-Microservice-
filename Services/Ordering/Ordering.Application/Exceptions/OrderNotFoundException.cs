using BuildingBlocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(object key) : base("Order", key)
        {
        }
    }
}
