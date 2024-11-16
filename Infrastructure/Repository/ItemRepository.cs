using Application.Common;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;
public class ItemRepository :IItemRepository
{
    private readonly ItemsDatabase _database;

    public ItemRepository(ItemsDatabase database)
    {
        _database = database;
    }
    public async Task CreateItem (Item item)
    {
        await _database.Items.AddAsync (item);
        await _database.SaveChangesAsync();
    }

    public async Task<Item> GetItemAsync(Guid? parentId)
    {
        var item=await _database.Items.FirstOrDefaultAsync(x=>x.ParentId==parentId);
        await _database.SaveChangesAsync();
        return item;
    }
}
