using BuildingBlocks.CQRS;
using Carter;
using Mapster;
using MediatR;

namespace Catalog.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Catageroy, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResponse>;
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Product", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var respone = result.Adapt<CreateProductResponse>();

                return Results.Created("", respone);
            });
        }
    }
}
