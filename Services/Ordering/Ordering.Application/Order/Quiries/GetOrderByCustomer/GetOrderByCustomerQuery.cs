using BuildingBlocks.CQRS;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrderByCustomer
{
    public record GetOrderByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerResult>;
    public record GetOrderByCustomerResult(IEnumerable<OrderDTO> OrderDTOs);

}
