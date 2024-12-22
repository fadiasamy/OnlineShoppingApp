using OnlineShoppingApp.APIs.Data.Models;
using OnlineShoppingApp.BL.Dtos;
using OnlineShoppingApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL;

    public class ItemService : IItemService
    {
    private readonly IItemsRepository _repository;

    public ItemService(IItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ItemReadDto>> GetItemsAsync()
    {
        var items = await _repository.GetItemsAsync();
        return items.Select(i => new ItemReadDto
        {
            Id = i.Id,
            ItemName = i.ItemName,
            Description = i.Description,
            UOM = i.Uom.UOMName,
            QTY = i.QTY,
            Price = i.Price
        });
    }

    public async Task<ItemReadDto> GetItemByIdAsync(int id)
    {
        var item = await _repository.GetItemByIdAsync(id);
        if (item == null) return null;

        return new ItemReadDto
        {
            Id = item.Id,
            ItemName = item.ItemName,
            Description = item.Description,
            UOM = item.Uom.UOMName,
            QTY = item.QTY,
            Price = item.Price
        };
    }

    public async Task CreateItemAsync(ItemAddDto createItemDto)
    {
        if (!await _repository.UOMExistsAsync(createItemDto.UomId))
        {
            throw new ArgumentException("Invalid UOM ID.");
        }

        var item = new Item
        {
            ItemName = createItemDto.ItemName,
            Description = createItemDto.Description,
            UomId = createItemDto.UomId,
            QTY = createItemDto.QTY,
            Price = createItemDto.Price
        };

        await _repository.AddItemAsync(item);
    }

    public async Task UpdateItemAsync(ItemUpdateDto updateItemDto)
    {
        var item = await _repository.GetItemByIdAsync(updateItemDto.Id);
        if (item == null)
        {
            throw new KeyNotFoundException("Item not found.");
        }

        if (!await _repository.UOMExistsAsync(updateItemDto.Id))
        {
            throw new ArgumentException("Invalid UOM ID.");
        }

        item.ItemName = updateItemDto.ItemName;
        item.Description = updateItemDto.Description;
        item.UomId = updateItemDto.UomId;
        item.QTY = updateItemDto.Quantity;
        item.Price = updateItemDto.Price;

        await _repository.UpdateItemAsync(item);
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _repository.GetItemByIdAsync(id);
        if (item == null)
        {
            throw new KeyNotFoundException("Item not found.");
        }

        await _repository.DeleteItemAsync(item);
    }
}




