using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Commands.UpdateOrder;

namespace Ordering.API.EndPoints
{
    public record UpdateOrderRequest(OrderDTO OrderDTO);
    public record UpdateOrderResponse(bool isUpdated);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/order", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);
            });
        }
    }
}
