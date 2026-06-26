using BuildingBlocks.CQRS;
using Carter;
using Mapster;
using MediatR;

namespace Catalog.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool isUpdated);
    public class UpdateProductEndPoint : ICarterModule
    {
        public  void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product", async (UpdateProductRequest request, ISender sender) => {

                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            }).ProducesProblem(StatusCodes.Status400BadRequest);
        }

    }
}
