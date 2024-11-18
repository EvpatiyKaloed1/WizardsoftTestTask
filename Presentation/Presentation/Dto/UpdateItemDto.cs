using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Dto;
public record  UpdateItemDto ( Guid ItemId, Guid? ParentId, string Name, List<UpdateItemDto> ChildItems)
{
    public Item MapToItem()
    {
        return new Item
        (           
            Name,
            ChildItems?.Select(item => item.MapToItem()).ToList() ?? [],
            ParentId,
            ItemId
        );
    }
}

