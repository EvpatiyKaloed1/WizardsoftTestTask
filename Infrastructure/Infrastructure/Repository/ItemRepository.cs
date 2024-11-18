using Application.Common;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository;

public class ItemRepository : IItemRepository
{
    private readonly ItemsDatabase _database;

    public ItemRepository(ItemsDatabase database)
    {
        _database = database;
    }

    public async Task CreateItemAsync(Item item, CancellationToken token)
    {
        await _database.AddRangeAsync(item.GetAll(), token);

        await _database.SaveChangesAsync(token);
    }

    public async Task<Item?> GetItemAsync(Guid id, CancellationToken token)
    {
        return await _database.Items
            .Include(x => x.ChildItems)
                .ThenInclude(x => x.ChildItems)
            .Include(x => x.Parent)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: token);
    }

    public async Task DeleteItemAsync(Guid id, CancellationToken token)
    {
        await _database.Items.Where(x => x.Id == id)
                             .ExecuteDeleteAsync(cancellationToken: token);
    }

    public async Task UpdateItemAsync(Item item, CancellationToken token)
    {
        await _database.Database.BeginTransactionAsync(token);

        await DeleteItemAsync(item.Id, token);

        await CreateItemAsync(item, token);

        await _database.Database.CommitTransactionAsync(token);
    }
}