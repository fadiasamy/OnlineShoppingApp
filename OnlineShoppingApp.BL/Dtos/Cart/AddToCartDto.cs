using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos.Cart
{
    public class AddToCartDto
    {
        public int UserId { get; set; }  
        public int ItemId { get; set; }  
        public int Quantity { get; set; }
    }
}
