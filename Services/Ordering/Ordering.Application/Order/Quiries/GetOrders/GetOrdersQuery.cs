using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>; 
    public record GetOrdersResult(PaginationResult<OrderDTO> PaginationResult);
}
