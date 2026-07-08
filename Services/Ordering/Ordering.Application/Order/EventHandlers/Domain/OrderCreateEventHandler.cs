using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.EventHandlers.Domain
{
    public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> logger) : INotificationHandler<AddOrderEvent>
    {
        public Task Handle(AddOrderEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handeld {notification.GetType().ToString()}");
            return Task.CompletedTask;
        }
    }
}
