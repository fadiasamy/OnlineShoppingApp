using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL.Repos.Currencies
{
    public interface ICurrencyExchangeRepository
    {
        Task SetExchangeRateAsync(string currencyCode, decimal exchangeRate, TimeSpan cacheDuration);
        Task<decimal?> GetExchangeRateAsync(string currencyCode);
    }
}
