using Domain;

namespace Application.Common;

public interface IItemRepository
{
    Task CreateItemAsync(Item item, CancellationToken token);

    Task DeleteItemAsync(Guid id, CancellationToken token);

    Task<Item?> GetItemAsync(Guid id, CancellationToken token);

    Task UpdateItemAsync(Item item, CancellationToken token);
}