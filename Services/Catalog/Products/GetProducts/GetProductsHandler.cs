using BuildingBlocks.CQRS;
using Catalog.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.Products.GetProducts
{
    public record GetProductQuery(int? PageNumber, int? PageSize) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductsHandler(IDocumentSession documentSession)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await documentSession.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10 ,cancellationToken);

            return new GetProductResult(result);
        }
    }
}
