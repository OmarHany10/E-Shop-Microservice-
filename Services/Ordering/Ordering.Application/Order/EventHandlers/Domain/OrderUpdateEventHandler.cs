using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.EventHandlers.Domain
{
    public class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> logger) : INotificationHandler<UpdateOrderEvent>
    {
        public Task Handle(UpdateOrderEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handeld {notification.GetType().ToString()}");
            return Task.CompletedTask;
        }
    }
}
