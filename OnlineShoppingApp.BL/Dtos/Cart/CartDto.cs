using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos.Cart
{
    public class CartDto
    {
        public int UserId { get; set; }  
        public decimal TotalPrice { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = new HashSet<CartItemDto>();

    }
}
