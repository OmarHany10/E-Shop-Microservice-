using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.Application.Order.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IAppDbContext context) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = context.Orders.FirstOrDefault(c => c.Id == OrderId.Of(request.OrderDTO.OrderId));

            if (order is null)
                throw new OrderNotFoundException(OrderId.Of(request.OrderDTO.OrderId));

            var updatedShippingAddress = Address.Of(request.OrderDTO.ShippingAddress.FirstName, request.OrderDTO.ShippingAddress.LastName, request.OrderDTO.ShippingAddress.EmailAddress, request.OrderDTO.ShippingAddress.AddressLine, request.OrderDTO.ShippingAddress.Country, request.OrderDTO.ShippingAddress.State, request.OrderDTO.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(request.OrderDTO.BillingAddress.FirstName, request.OrderDTO.BillingAddress.LastName, request.OrderDTO.BillingAddress.EmailAddress, request.OrderDTO.BillingAddress.AddressLine, request.OrderDTO.BillingAddress.Country, request.OrderDTO.BillingAddress.State, request.OrderDTO.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(request.OrderDTO.Payment.CardName, request.OrderDTO.Payment.CardNumber, request.OrderDTO.Payment.Expiration, request.OrderDTO.Payment.Cvv, request.OrderDTO.Payment.PaymentMethod);


            order.Update(
                orderName: OrderName.Of(request.OrderDTO.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: request.OrderDTO.Status
                );

            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }
    }
}
