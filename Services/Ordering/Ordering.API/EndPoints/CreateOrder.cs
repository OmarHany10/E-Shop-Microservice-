using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Commands.CreateOrder;

namespace Ordering.API.EndPoints
{
    public record CreateOrderRequest(OrderDTO OrderDTO);
    public record CreateOrderResponse(Guid Id);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/order", async (CreateOrderRequest requet, ISender sender) =>
            {
                var command = requet.Adapt<CreateOrderCommand>();

                var result = await sender.Send(command);
                var response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/order/{response.Id}", response);
            });
        }
    }
}
