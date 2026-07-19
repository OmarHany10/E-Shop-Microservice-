
using Basket.API.DTOs;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.CheckoustBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDTO BasketCheckoutDTO);
    public record CheckoutBasketResponse(bool isSuccess);
    public class CheckoutBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                //var command = request.Adapt<CheckoutBasketCommand>();

                var command = new CheckoutBasketCommand(request.BasketCheckoutDTO);

                var result  = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            });
        }
    }
}
