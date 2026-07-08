using BuildingBlocks.CQRS;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrderByName
{
    public record GetOrderByNameQuery(string name): IQuery<GetOrderByNameResult>;
    public record GetOrderByNameResult(IEnumerable<OrderDTO> OrderDTOs);
}
