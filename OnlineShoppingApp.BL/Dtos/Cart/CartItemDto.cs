using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos.Cart
{
    public class CartItemDto
    {
        public int ItemId { get; set; }  
        public string ItemName { get; set; } =string.Empty;
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
