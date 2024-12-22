using OnlineShoppingApp.APIs.Data.Models;
using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL.Repos.Orders
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> CloseOrderAsync(int id);
        Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
