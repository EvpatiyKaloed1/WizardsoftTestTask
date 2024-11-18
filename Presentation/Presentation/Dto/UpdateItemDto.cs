using Domain;

namespace Presentation.Dto;
public record UpdateItemDto(Guid ItemId, Guid? ParentId, string Name, List<UpdateItemDto> ChildItems)
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