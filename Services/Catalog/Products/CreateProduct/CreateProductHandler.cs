using BuildingBlocks.CQRS;
using Catalog.Models;
using Mapster;
using MediatR;

namespace Catalog.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Catageroy, string Description, string ImageFile, decimal Price): ICommand<CreateProductResponse>;
    public record CreateProductResponse(Guid Id);
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Catageroy = request.Catageroy,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            return  new CreateProductResponse(Guid.NewGuid());
        }
    }
}
