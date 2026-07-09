using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Order.Commands.DeleteOrder;

namespace Ordering.API.EndPoints
{
    public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool isDeleted);

    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/order/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new DeleteOrderCommand(id));
                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            });
        }
    }
}
