using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp.APIs.Data.Context;
using OnlineShoppingApp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL.Repos.Cart
{
    public class CartRepo : ICartRepo
    {
        private readonly MyAppDBContext _context;

        public CartRepo(MyAppDBContext context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartItemAsync(int userId, int itemId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ItemId == itemId);
        }

        public async Task<bool> AddToCartAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }

}
