using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Quiries.GetOrderByName;

namespace Ordering.API.EndPoints
{
    public record GetOrderByNameResponse(IEnumerable<OrderDTO> OrderDTOs);
    public class GetOrderByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{name}", async (string name, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderByNameQuery(name));
                var response = result.Adapt<GetOrderByNameResponse>();

                return Results.Ok(response);
            });
        }
    }
}
