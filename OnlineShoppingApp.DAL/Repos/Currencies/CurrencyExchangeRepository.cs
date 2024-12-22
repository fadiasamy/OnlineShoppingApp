using Microsoft.Extensions.Caching.Distributed;

namespace OnlineShoppingApp.DAL.Repos.Currencies
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly IDistributedCache _cache;

        public CurrencyExchangeRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetExchangeRateAsync(string currencyCode, decimal exchangeRate, TimeSpan cacheDuration)
        {
            var cacheKey = $"ExchangeRate_{currencyCode.ToUpper()}";
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration
            };

            await _cache.SetStringAsync(cacheKey, exchangeRate.ToString(), cacheOptions);
        }

        public async Task<decimal?> GetExchangeRateAsync(string currencyCode)
        {
            var cacheKey = $"ExchangeRate_{currencyCode.ToUpper()}";
            var exchangeRateString = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(exchangeRateString))
            {
                return null;
            }

            if (decimal.TryParse(exchangeRateString, out var exchangeRate))
            {
                return exchangeRate;
            }

            return null;
        }
    }
}
