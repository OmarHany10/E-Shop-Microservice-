using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Domain.ValueObjects;


namespace Ordering.Application.Order.Commands.CreateOrder
{
    public class CreateOrderHandler(IAppDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var shippingAdd = Address.Of(request.OrderDTO.ShippingAddress.FirstName, request.OrderDTO.ShippingAddress.LastName, request.OrderDTO.ShippingAddress.EmailAddress, request.OrderDTO.ShippingAddress.AddressLine, request.OrderDTO.ShippingAddress.Country, request.OrderDTO.ShippingAddress.State, request.OrderDTO.ShippingAddress.ZipCode);
            var billingAdd = Address.Of(request.OrderDTO.BillingAddress.FirstName, request.OrderDTO.BillingAddress.LastName, request.OrderDTO.BillingAddress.EmailAddress, request.OrderDTO.BillingAddress.AddressLine, request.OrderDTO.BillingAddress.Country, request.OrderDTO.BillingAddress.State, request.OrderDTO.BillingAddress.ZipCode);

            var order = Domain.Models.Order.Create(
                orderId: request.OrderDTO.OrderId,
                customerId: CustomerId.Of(request.OrderDTO.CustomerId),
                orderName: OrderName.Of(request.OrderDTO.OrderName),
                shippingAddress: shippingAdd,
                billingAddress: billingAdd,
                payment: Payment.Of(request.OrderDTO.Payment.CardName, request.OrderDTO.Payment.CardNumber, request.OrderDTO.Payment.Expiration, request.OrderDTO.Payment.Cvv, request.OrderDTO.Payment.PaymentMethod)
                );

            foreach(var item in request.OrderDTO.OrderItems)
            {
                order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            context.Orders.Add(order);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }
    }
}
