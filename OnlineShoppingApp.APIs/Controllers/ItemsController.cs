using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.BL.Services.Cart;
using OnlineShoppingApp.BL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShoppingApp.BL.Dtos.Cart;

namespace OnlineShoppingApp.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ICartService _cartService;

        public ItemsController(IItemService itemService, ICartService cartService)
        {
            _itemService = itemService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItems()
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemReadDto>> GetItem(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { Message = "Item not found." });
            }
            return Ok(item);
        }

        [HttpPost("{id}/add-to-cart")]
        public async Task<ActionResult> AddToCart(int id, [FromBody] AddToCartDto addToCartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { Message = "Item not found." });
            }

            var result = await _cartService.AddToCartAsync(addToCartDto.UserId, id, addToCartDto.Quantity);
            if (result)
            {
                return Ok(new { Message = "Item added to cart successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Failed to add item to cart." });
            }
        }

        [HttpGet("cart/{userId}")]
        public async Task<ActionResult<CartDto>> GetCartItems(int userId)
        {
            var cartItems = await _cartService.GetCartItemsByUserIdAsync(userId);
            if (cartItems == null)
            {
                return NotFound(new { Message = "No items found in cart." });
            }

            decimal totalCartValue = cartItems.Sum(item => item.TotalPrice);

            var cartDto = new CartDto
            {
                UserId = userId,
                Items = (ICollection<CartItemDto>)cartItems,
                TotalPrice = totalCartValue
            };

            return Ok(cartDto);
        }

    }
}
