using OnlineShoppingApp.APIs.Data.Models;
using OnlineShoppingApp.BL.Dtos;
using OnlineShoppingApp.DAL.Repos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return orders.Select(o => new OrderDto
            {
                CustomerId = o.CustomerId,
                DiscountPromoCode = o.DiscountPromoCode,
                DiscountValue = o.DiscountValue,
                TotalPrice = o.TotalPrice,
                CurrencyCode = o.CurrencyCode,
                Status = o.Status,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                {
                    ItemId = od.ItemId,
                    ItemPrice = od.ItemPrice,
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return null;

            return new OrderDto
            {
                CustomerId = order.CustomerId,
                DiscountPromoCode = order.DiscountPromoCode,
                DiscountValue = order.DiscountValue,
                TotalPrice = order.TotalPrice,
                CurrencyCode = order.CurrencyCode,
                Status = order.Status,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ItemId = od.ItemId,
                    ItemPrice = od.ItemPrice,
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice
                }).ToList()
            };
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                DiscountPromoCode = orderDto.DiscountPromoCode,
                DiscountValue = orderDto.DiscountValue,
                TotalPrice = orderDto.TotalPrice,
                CurrencyCode = orderDto.CurrencyCode,
                Status = "Open",
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetail
                {
                    ItemId = od.ItemId,
                    ItemPrice = od.ItemPrice,
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice
                }).ToList()
            };

            var createdOrder = await _orderRepository.CreateOrderAsync(order);

            return new OrderDto
            {
                CustomerId = createdOrder.CustomerId,
                DiscountPromoCode = createdOrder.DiscountPromoCode,
                DiscountValue = createdOrder.DiscountValue,
                TotalPrice = createdOrder.TotalPrice,
                CurrencyCode = createdOrder.CurrencyCode,
                Status = createdOrder.Status,
                OrderDetails = createdOrder.OrderDetails.Select(od => new OrderDetailDto
                {
                    ItemId = od.ItemId,
                    ItemPrice = od.ItemPrice,
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice
                }).ToList()
            };
        }

        public async Task<bool> CloseOrderAsync(int id)
        {
            return await _orderRepository.CloseOrderAsync(id);
        }

        public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(o => new OrderDto
            {
                CustomerId = o.CustomerId,
                DiscountPromoCode = o.DiscountPromoCode,
                DiscountValue = o.DiscountValue,
                TotalPrice = o.TotalPrice,
                CurrencyCode = o.CurrencyCode,
                Status = o.Status,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                {
                    ItemId = od.ItemId,
                    ItemPrice = od.ItemPrice,
                    Quantity = od.Quantity,
                    TotalPrice = od.TotalPrice
                }).ToList()
            }).ToList();
        }

    }
}
