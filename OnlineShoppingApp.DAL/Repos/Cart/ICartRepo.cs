using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL.Repos.Cart
{
    public interface ICartRepo
    {
        Task<CartItem> GetCartItemAsync(int userId, int itemId);
        Task<bool> AddToCartAsync(CartItem cartItem);
    }
}
