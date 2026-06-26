using BuildingBlocks.CQRS;
using Carter;
using Mapster;
using MediatR;

namespace Catalog.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Product", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                return Results.Created("", result);
            });
        }
    }
}
