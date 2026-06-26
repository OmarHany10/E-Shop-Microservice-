using BuildingBlocks.CQRS;
using Catalog.Models;
using Marten;

namespace Catalog.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isDeleted);
    public class DeleteProductHandler(IDocumentSession documentSession)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            documentSession.Delete<Product>(request.id);
            await documentSession.SaveChangesAsync(cancellationToken);
            
            return new DeleteProductResult(true);
        }
    }
}
