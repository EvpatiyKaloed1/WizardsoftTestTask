using Domain;

namespace Presentation.Dto;

public record ItemDto (string Name,List<ItemDto> ChildItems);
