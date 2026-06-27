using Carter;
using Catalog.Products.CreateProduct;
using FluentValidation;
using Mapster;
using MediatR;

namespace Catalog.Products.DeleteProduct
{
    public record DeleteProductResponse(bool isDeleted);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(c => c.id).NotEmpty().WithMessage("Id must be not empty");
            
        }
    }
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
