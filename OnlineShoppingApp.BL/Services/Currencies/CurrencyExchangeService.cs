using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using OnlineShoppingApp.DAL.Data.Models;
using OnlineShoppingApp.DAL.Repos.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.Currencies
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeRepository _repository;
        private readonly IConfiguration _configuration;

        public CurrencyExchangeService(ICurrencyExchangeRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task SetExchangeRateAsync(string currencyCode, decimal exchangeRate)
        {
            var cacheDuration = TimeSpan.FromSeconds(Convert.ToDouble(_configuration["Redis:DefaultCacheDuration"]));
            await _repository.SetExchangeRateAsync(currencyCode, exchangeRate, cacheDuration);
        }

        public async Task<decimal?> GetExchangeRateAsync(string currencyCode)
        {
            return await _repository.GetExchangeRateAsync(currencyCode);
        }
    }
}
