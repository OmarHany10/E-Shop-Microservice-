using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extension;
using Ordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.EventHandlers.Domain
{
    public class OrderCreateEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager,ILogger<OrderCreateEventHandler> logger) 
        : INotificationHandler<AddOrderEvent>
    {
        public async Task Handle(AddOrderEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handeld {notification.GetType().ToString()}");


            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderDto = notification.Order.ToOrderDto();
                await publishEndpoint.Publish(orderDto, cancellationToken);
            }
            
        }
    }
}
