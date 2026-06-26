using Carter;
using Catalog.Models;
using Mapster;
using MediatR;

namespace Catalog.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async(ISender sender) => 
            {

                var result = await sender.Send(new GetProductQuery());

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }).ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
