using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.DTOs
{
    public record OrderItemDTO(Guid OrderId, Guid ProductId, int Quantity, decimal Price);
}
