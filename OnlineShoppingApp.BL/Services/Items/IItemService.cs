using OnlineShoppingApp.APIs.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;


public interface IItemService
{
    Task<IEnumerable<ItemReadDto>> GetItemsAsync();
    Task<ItemReadDto> GetItemByIdAsync(int id);
    Task CreateItemAsync(ItemAddDto addItemDto);
    Task UpdateItemAsync(ItemUpdateDto updateItemDto);
    Task DeleteItemAsync(int id);


}
