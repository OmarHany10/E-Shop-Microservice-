using BuildingBlocks.CQRS;
using Catalog.Exceptions;
using Catalog.Models;
using Marten;

namespace Catalog.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Categeroy, string Description, string ImageFile, decimal Price): ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool isUpdated);
    public class UpdateProductHandler(IDocumentSession documentSession)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id);

            if (product is null)
                throw new ProductNotFoundException();

            product.Categeroy = request.Categeroy;
            product.ImageFile = request.ImageFile;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Name = request.Name;

            documentSession.Update(product);
            await documentSession.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
