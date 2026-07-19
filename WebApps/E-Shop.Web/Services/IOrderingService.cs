using E_Shop.Web.Models.Ordering;
using Refit;

namespace E_Shop.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?PageNumebr={PageNumebr}&PageSize={PageSize}\"")]
        Task<GetOrdersResponse> GetOrders(int? PageNumebr = 1, int? PageSize = 10);

        [Get("/ordering-service/orders/{name}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string name);

        [Get("/ordering-service/orders/customer/{id}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid id);
    }
}
