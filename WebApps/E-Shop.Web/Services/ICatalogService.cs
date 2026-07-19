using E_Shop.Web.Models.Catalog;
using Refit;

namespace E_Shop.Web.Services
{
    public interface ICatalogService
    {
        [Get("/catalog-service/products?PageNumber={PageNumber}&PageSize={PageSize}")]
        Task<GetProductsResponse> GetProducts(int? PageNumber = 1, int? PageSize = 10);

        [Get("/catalog-service/product/{id}")]
        Task<GetProductByIdResponse> GetProduct(Guid id);

        [Get("/catalog-service/product/category/{category}")]
        Task<GetProductByCategoryResponse> GetProductsByCategory(string category);
    }
}
