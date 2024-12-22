using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp.APIs.Data.Context;
using OnlineShoppingApp.APIs.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.DAL;

public class ItemsRepository : IItemsRepository
{
    private readonly MyAppDBContext _context;

    public ItemsRepository(MyAppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await _context.Items.Include(i => i.Uom).ToListAsync();
    }

    public async Task<Item> GetItemByIdAsync(int id)
    {
        return await _context.Items.Include(i => i.Uom).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddItemAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(Item item)
    {
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UOMExistsAsync(int uomId)
    {
        return await _context.UOMs.AnyAsync(u => u.Id == uomId);
    }
}
