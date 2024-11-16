using Application.Commands.Create;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;
[Route("item-controller")]
public class ItemController: Controller
{
    private readonly ISender _sender;

    public ItemController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("create")]
    public async Task CreateItemAsync([FromBody] ItemDto dto)
    {
        var item = MapToItem(dto);
        await _sender.Send(new CreateItemCommand(item.Name, item.ChildItems));
    }

    private Item MapToItem(ItemDto dto)
    {
        var a = dto.ChildItems?.Select(MapToItem).ToList();
        return new Item(
            dto.Name,a
             
        );
    }
}
