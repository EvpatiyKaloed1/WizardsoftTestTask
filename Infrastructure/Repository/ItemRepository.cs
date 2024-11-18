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

    public async Task CreateItemAsync (Item item)
    {
        await _database.Items.AddAsync(item);
        await _database.SaveChangesAsync();
    }

    public async Task<Item> GetItemAsync(Guid? id)
    {
        var item = await _database.Items
            .Include(x => x.ChildItems)
            .Include(x => x.Parent)
            .FirstOrDefaultAsync(x => x.Id == id);

        return item;
    }

    public async Task DeleteItemAsync(Guid id)
    {
        await _database.Items.Where(x => x.Id == id)
                             .ExecuteDeleteAsync();
    }

    public async Task UpdateItemAsync(Item item)
    {
        await _database.Database.BeginTransactionAsync();

        await DeleteItemAsync(item.Id);

        await CreateItemAsync(item);

        await _database.Database.CommitTransactionAsync();
    }
}
