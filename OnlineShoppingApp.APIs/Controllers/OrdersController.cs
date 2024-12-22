using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShoppingApp.BL.Dtos;
using OnlineShoppingApp.BL.Services.Orders;
using OnlineShoppingApp.BL.Services.Currencies;
using OnlineShoppingApp.BL.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingApp.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICurrencyExchangeService _currencyService;
        private readonly IConfiguration _configuration;

        public OrdersController(
            IOrderService orderService,
            IUserService userService,
            ICurrencyExchangeService currencyService,
            IConfiguration configuration)
        {
            _orderService = orderService;
            _userService = userService;
            _currencyService = currencyService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByCustomerId(string customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderDto orderDto)
        {
            var user = await _userService.GetUserByIdAsync(orderDto.CustomerId); 
            if (user == null)
            {
                return Unauthorized("User not registered.");
            }

            var roles = await _userService.GetUserRolesAsync(user); 
            if (!roles.Contains("Customer"))
            {
                return Unauthorized("User does not have the required role.");
            }

            var discountPromoCode = _configuration["DiscountPromoCode"];
            var discountValue = Convert.ToDecimal(_configuration["DiscountValue"]);

            if (!string.IsNullOrEmpty(orderDto.DiscountPromoCode) && orderDto.DiscountPromoCode == discountPromoCode)
            {
                orderDto.DiscountValue = discountValue;
            }

            var currencyCode = _configuration["BasicCurrency"];
            var exchangeRate = await _currencyService.GetExchangeRateAsync(currencyCode);

            if (orderDto.CurrencyCode != currencyCode)
            {
                var currencyExchangeRate = await _currencyService.GetExchangeRateAsync(orderDto.CurrencyCode);
                orderDto.ExchangeRate = (decimal)currencyExchangeRate;
            }
            else
            {
                orderDto.ExchangeRate = 1;
            }

            decimal totalPriceInOriginalCurrency = orderDto.OrderDetails.Sum(item => item.TotalPrice);
            decimal totalPriceAfterDiscount = totalPriceInOriginalCurrency - orderDto.DiscountValue;
            decimal finalPrice = totalPriceAfterDiscount * orderDto.ExchangeRate;

            orderDto.TotalPrice = finalPrice;

            var createdOrder = await _orderService.CreateOrderAsync(orderDto);

            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.CustomerId }, createdOrder);
        }

        [HttpPut("close/{id}")]
        public async Task<ActionResult> CloseOrder(int id)
        {
            var result = await _orderService.CloseOrderAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
