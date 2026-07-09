using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extension;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrderByName
{
    public class GetOrderByNameHandler(IAppDbContext context)
        : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
        {
            var orderName = OrderName.Of(request.name);

            var orders = await context.Orders.Include(o => o.OrderItems)
                .Where(o => o.OrderName.Value.Contains(request.name))
                .ToListAsync(cancellationToken);

            var result = orders.ToOrderDtoList();
            return new GetOrderByNameResult(result);
        }
    }
}
