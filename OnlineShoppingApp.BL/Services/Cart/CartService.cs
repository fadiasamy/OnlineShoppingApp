using OnlineShoppingApp.DAL.Data.Models;
using OnlineShoppingApp.DAL.Data;
using OnlineShoppingApp.BL.Dtos.Cart;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShoppingApp.APIs.Data.Context;

namespace OnlineShoppingApp.BL.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly MyAppDBContext _context;

        public CartService(MyAppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToCartAsync(int itemId, int userId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ItemId == itemId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                _context.CartItems.Update(cartItem);
            }
            else
            {
                var newCartItem = new CartItem
                {
                    UserId = userId,
                    ItemId = itemId,
                    Quantity = quantity
                };
                await _context.CartItems.AddAsync(newCartItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Item) 
                .ToListAsync();

            var cartItemDtos = cartItems.Select(cartItem => new CartItemDto
            {
                ItemId = cartItem.ItemId,
                ItemName = cartItem.Item.ItemName, 
                Quantity = cartItem.Quantity,
                TotalPrice = cartItem.Item.Price 
            });

            return cartItemDtos;
        }
    }
}
