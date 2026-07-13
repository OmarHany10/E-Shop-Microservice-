using Basket.API.Data;
using Basket.API.DTOs;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using MassTransit.Testing;

namespace Basket.API.CheckoustBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDTO) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool isSuccess);
    public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.BasketCheckoutDTO.UserName, cancellationToken);
            if (basket == null)
                return new CheckoutBasketResult(false);

            var eventBasket = request.BasketCheckoutDTO.Adapt<BasketCheckoutEvent>();
            eventBasket.TotalPrice = basket.TotalPrice;

            eventBasket.Carts = basket.Items.Adapt<List<Cart>>();

            await publishEndpoint.Publish(eventBasket, cancellationToken);

            await basketRepository.DeleteBasket(basket.UserName);

            return new CheckoutBasketResult(true);

        }
    }
}
