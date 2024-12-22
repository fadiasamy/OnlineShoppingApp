using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

    public class OrderDto
    {
    public string CustomerId { get; set; }
    public string DiscountPromoCode { get; set; } = string.Empty;  
    public decimal DiscountValue { get; set; } 
    public decimal TotalPrice { get; set; }  
    public string CurrencyCode { get; set; } = string.Empty;  
    public string Status { get; set; } = string.Empty; 
    public decimal ExchangeRate { get; set; } 
    public ICollection<OrderDetailDto> OrderDetails { get; set; } = new HashSet<OrderDetailDto>();
}

