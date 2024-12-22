using OnlineShoppingApp.APIs.Data.Models;
using OnlineShoppingApp.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.Orders
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<bool> CloseOrderAsync(int id);
        Task<List<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
