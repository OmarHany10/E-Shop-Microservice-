using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Commands.CreateOrder;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Order.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation($"Integration Event handled: {context.Message.GetType().Name}");

            var command = MapToCreateOrderCommand(context.Message);

            await sender.Send(command);
        }


        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            // Create full order with incoming event data
            var addressDto = new AddressDTO(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDTO(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();

            List<OrderItemDTO> orderItems = new List<OrderItemDTO>();

            foreach(var item in message.Carts)
            {
                orderItems.Add(new OrderItemDTO(orderId, item.ProductId, item.Quantity, item.Price));

            }

            var orderDto = new OrderDTO() {
                OrderId = orderId,
                CustomerId = message.CustomerId,
                OrderName = message.UserName,
                ShippingAddress = addressDto,
                BillingAddress = addressDto,
                Payment = paymentDto,
                Status = Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems =orderItems
            };

            return new CreateOrderCommand(orderDto);
        }
    }
}
