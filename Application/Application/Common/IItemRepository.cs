using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public interface IItemRepository
{
    Task CreateItem(Item item);
    Task <Item> GetItemAsync(Guid? parentId);
}
