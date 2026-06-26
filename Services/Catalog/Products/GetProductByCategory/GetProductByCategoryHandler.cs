using BuildingBlocks.CQRS;
using Catalog.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession documentSession)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await documentSession.Query<Product>().
                Where(p => p.Categeroy.Contains(request.Category)).ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(result);
        }
    }
}
