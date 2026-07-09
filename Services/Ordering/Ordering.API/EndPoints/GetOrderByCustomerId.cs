using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Order.Quiries.GetOrderByCustomer;

namespace Ordering.API.EndPoints
{
    //public record GetOrderByCustomerRequest(Guid CustomerId);
    public record GetOrderByCustomerResponse(IEnumerable<OrderDTO> OrderDTOs);
    public class GetOrderByCustomerId : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetOrderByCustomerQuery(id));
                var response = result.Adapt<GetOrderByCustomerResponse>();

                return Results.Ok(response);
            });
        }
    }
}
