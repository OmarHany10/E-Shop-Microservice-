using Carter;
using Mapster;
using MediatR;

namespace Catalog.Products.DeleteProduct
{
    public record DeleteProductResponse(bool isDeleted);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResult>();

                return Results.Ok(response);
            });
        }
    }
}
