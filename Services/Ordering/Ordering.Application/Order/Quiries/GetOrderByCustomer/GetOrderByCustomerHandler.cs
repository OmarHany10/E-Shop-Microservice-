using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extension;
using Ordering.Application.Order.Quiries.GetOrderByName;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Quiries.GetOrderByCustomer
{
    public class GetOrderByCustomerHandler(IAppDbContext context) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders.Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            var result = orders.ToOrderDtoList();
            return new GetOrderByCustomerResult(result);

        }
    }

}
