using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extension;
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
            var orders = await context.Orders.Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(request.name))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            var result = orders.ToOrderDtoList();
            return new GetOrderByNameResult(result);
        }
    }
}
