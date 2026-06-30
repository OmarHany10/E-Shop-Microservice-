using Carter;
using Catalog.Models;
using Mapster;
using MediatR;

namespace Catalog.Products.GetProducts
{
    public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async([AsParameters] GetProductRequest request, ISender sender) => 
            {
                var query = request.Adapt<GetProductQuery>();
                var result = await sender.Send(new GetProductQuery(request.PageNumber, request.PageSize));

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }).ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
