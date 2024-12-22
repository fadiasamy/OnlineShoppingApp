using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.Currencies
{
    public interface ICurrencyExchangeService
    {
        Task SetExchangeRateAsync(string currencyCode, decimal exchangeRate);
        Task<decimal?> GetExchangeRateAsync(string currencyCode);
    }
}
