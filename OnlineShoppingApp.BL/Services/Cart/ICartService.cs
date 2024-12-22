using OnlineShoppingApp.BL.Dtos.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Services.Cart
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(int itemId, int userId, int quantity);
        Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(int userId);


    }
}
