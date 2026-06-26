using BuildingBlocks.CQRS;
using Catalog.Models;
using Mapster;
using Marten;
using MediatR;

namespace Catalog.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price): ICommand<CreateProductResponse>;
    public record CreateProductResponse(Guid Id);
    public class CreateProductHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Categeroy = request.Categeroy,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            return  new CreateProductResponse(Guid.NewGuid());
        }
    }
}
