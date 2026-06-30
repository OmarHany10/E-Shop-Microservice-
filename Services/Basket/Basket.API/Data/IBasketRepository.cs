using Basket.API.Models;

namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppinCart> GetBasket(string userName, CancellationToken cancellationToken = default);
        Task<ShoppinCart> StoreBasket(ShoppinCart shoppinCart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}
