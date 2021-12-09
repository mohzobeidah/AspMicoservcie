using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCacheCache = redisCache ?? throw new ArgumentNullException(nameof(BasketRepository));
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCacheCache.RemoveAsync(username);
        }

        public async  Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCacheCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
            {
                return null; 

            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async  Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _redisCacheCache.SetStringAsync(shoppingCart.Username , JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.Username);
        }
    }
}
