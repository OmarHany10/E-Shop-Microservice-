using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CashedBasketRepository(IDistributedCache cache, IBasketRepository basketRepository)
        : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            bool result = await basketRepository.DeleteBasket(userName, cancellationToken);
            if (result)
                await cache.RemoveAsync(userName, cancellationToken);
            return result;
        }

        public async Task<ShoppinCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basketFromCashe = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(basketFromCashe))
                return JsonSerializer.Deserialize<ShoppinCart>(basketFromCashe);

            var basket = await basketRepository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize<ShoppinCart>(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppinCart> StoreBasket(ShoppinCart shoppinCart, CancellationToken cancellationToken = default)
        {
            var basket = await basketRepository.StoreBasket(shoppinCart, cancellationToken);
            
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShoppinCart>(basket), cancellationToken);
            return basket;
        }
    }
}
