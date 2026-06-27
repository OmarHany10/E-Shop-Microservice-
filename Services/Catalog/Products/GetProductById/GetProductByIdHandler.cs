using BuildingBlocks.CQRS;
using Catalog.Exceptions;
using Catalog.Models;
using Marten;

namespace Catalog.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await documentSession.LoadAsync<Product>(request.id, cancellationToken);

            if (result == null)
                throw new ProductNotFoundException(request.id);

            return new GetProductByIdResult(result);
        }
    }
}
