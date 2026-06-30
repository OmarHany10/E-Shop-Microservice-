using Basket.API.Exceptions;
using Basket.API.Models;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppinCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppinCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppinCart>(userName, cancellationToken);
            return basket is not null ? basket : throw new BasketNotFoundException(userName);
        }

        public async Task<ShoppinCart> StoreBasket(ShoppinCart shoppinCart, CancellationToken cancellationToken = default)
        {
            session.Store<ShoppinCart>(shoppinCart);
            await session.SaveChangesAsync(cancellationToken);
            return shoppinCart;
        }
    }
}
