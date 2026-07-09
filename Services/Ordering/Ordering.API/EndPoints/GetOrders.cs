using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Quiries.GetOrders;

namespace Ordering.API.EndPoints
{
    public record GetOrdersResponse(PaginationResult<OrderDTO> PaginationResult);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            });
        }
    }
}
