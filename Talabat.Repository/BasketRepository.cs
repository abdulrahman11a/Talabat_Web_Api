using StackExchange.Redis;
using System.Text.Json;
using Talabat.core.Entitys;
using Talabat.core.Repositories.Contract;

namespace Talabat.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<bool> DeleteAllAsync(string basketId)
            => await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basketData = await _database.StringGetAsync(basketId);
            return basketData.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basketData);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var basketSerialized = JsonSerializer.Serialize(basket);
            var isSetSuccessful = await _database.StringSetAsync(basket.Id, basketSerialized,TimeSpan.FromDays(30));
            return isSetSuccessful ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
