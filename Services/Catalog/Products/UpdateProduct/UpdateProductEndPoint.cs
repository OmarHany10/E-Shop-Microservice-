using BuildingBlocks.CQRS;
using Carter;
using Catalog.Products.DeleteProduct;
using FluentValidation;
using Mapster;
using MediatR;

namespace Catalog.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool isUpdated);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id must be not empty");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name must be not empty");
            RuleFor(c => c.Categeroy).NotEmpty().WithMessage("Category must be not empty");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description must be not empty");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be > 0");

        }
    }
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
