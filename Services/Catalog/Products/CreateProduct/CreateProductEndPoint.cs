using BuildingBlocks.CQRS;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;

namespace Catalog.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price);

    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name must be not empty");
            RuleFor(p => p.Categeroy).NotEmpty().WithMessage("Category must be not empty");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description must be not empty");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be > 0");
        }
    }
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
