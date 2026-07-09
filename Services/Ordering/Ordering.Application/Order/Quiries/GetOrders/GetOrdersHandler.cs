using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrders
{
    public class GetOrdersHandler(IAppDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders.AsNoTracking()
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip((request.PaginationRequest.PageNumebr - 1) * request.PaginationRequest.PageSize)
                .Take(request.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);

            var count = context.Orders.Count();
            var result = new PaginationResult<OrderDTO>()
            {
                PageNumber = request.PaginationRequest.PageNumebr,
                PageSize = request.PaginationRequest.PageSize,
                Count = count,
                data = orders.ToOrderDtoList().ToList()
            };

            return new GetOrdersResult(result);
        }
    }
}
