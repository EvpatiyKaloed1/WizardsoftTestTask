using Application.Commands.Update;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public interface IItemRepository
{
    Task CreateItemAsync(Item item);
    Task DeleteItemAsync(Guid id);
    Task <Item> GetItemAsync(Guid? parentId);
    Task UpdateItemAsync(Item item);
}
