using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.BL.Dtos;
using OnlineShoppingApp.BL.Services.Currencies;
using OnlineShoppingApp.DAL.Data.Models;

namespace OnlineShoppingApp.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencuExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _service;
        private readonly ILogger<CurrencuExchangeController> _logger;

        public CurrencuExchangeController(ICurrencyExchangeService service, ILogger<CurrencuExchangeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("SetExchangeRate")]
        public async Task<IActionResult> SetExchangeRate([FromBody] ExchangeRateDto exchangeRateDto)
        {
            if (string.IsNullOrEmpty(exchangeRateDto.CurrencyCode) || exchangeRateDto.ExchangeRate <= 0)
            {
                return BadRequest(new { Message = "Invalid currency code or exchange rate." });
            }

            await _service.SetExchangeRateAsync(exchangeRateDto.CurrencyCode, exchangeRateDto.ExchangeRate);
            _logger.LogInformation($"Exchange rate for {exchangeRateDto.CurrencyCode} set to {exchangeRateDto.ExchangeRate}.");

            return Ok(new { Message = "Exchange rate saved successfully." });
        }

        [HttpGet("GetExchangeRate")]
        public async Task<IActionResult> GetExchangeRate([FromQuery] GetExchangeRateRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.CurrencyCode))
            {
                return BadRequest(new { Message = "Currency code is required." });
            }

            var exchangeRate = await _service.GetExchangeRateAsync(requestDto.CurrencyCode);

            if (exchangeRate == null)
            {
                return NotFound(new { Message = "Exchange rate not found for the given currency code." });
            }

            return Ok(new { CurrencyCode = requestDto.CurrencyCode, ExchangeRate = exchangeRate });
        }
    }
}
