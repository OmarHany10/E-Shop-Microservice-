using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IAppDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = context.Orders.FirstOrDefault(c => c.Id == OrderId.Of(request.Id));

            if (order is null)
                throw new OrderNotFoundException(OrderId.Of(request.Id));

            context.Orders.Remove(order);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
