using Domain;

namespace Presentation.Dto;

public record ItemDto(string Name, List<ItemDto> ChildItems)
{
    public Item MapToItem()
    {
        return new Item
        (
            Name,
            ChildItems?.Select(item => item.MapToItem()).ToList() ?? []
        );
    }
}