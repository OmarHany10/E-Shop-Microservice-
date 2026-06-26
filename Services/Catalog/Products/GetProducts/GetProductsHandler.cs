using BuildingBlocks.CQRS;
using Catalog.Models;
using Marten;

namespace Catalog.Products.GetProducts
{
    public record GetProductQuery() : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductsHandler(IDocumentSession documentSession)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await documentSession.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductResult(result);
        }
    }
}
