using Application.Commands.Create;
using Application.Commands.Update;
using Application.Queries.Delete;
using Application.Queries.GetItem;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers;


[Route("items")]
public class ItemController : Controller
{
    private readonly ISender _sender;

    public ItemController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("get/{id:guid}")]
    public async Task<Item> GetItem(Guid id)
    {
        return await _sender.Send(new GetItemQuery(id));
    }

    [SwaggerOperation(Description =
        "Пример json" + """
        {
            "Name": "Root Item",
            "childItems": [
                {
                    "Name": "Child 1"
                },
                {
                    "Name": "Child 2",
                    "childItems": [
                    {
                        "Name": "Child 3",
                        "childItems": [
                        {
                            "Name": "Child 1"
                        }]
                    }]
                }
            ]
        }  
    """)]
    [HttpPost("create")]
    public async Task<Item> CreateItemAsync([FromBody] ItemDto dto)
    {
        var item = dto.MapToItem();

        return await _sender.Send(new CreateItemCommand(item.Name, item.ChildItems));
    }

    [HttpPut("update")]
    public async Task<Item> UpdateItemAsync([FromBody] UpdateItemDto dto)
    {
        var item = dto.MapToItem();

        return await _sender.Send(new UpdateItemCommand(item.ParentId, item.Id, item.Name, item.ChildItems));
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task DeleteItemAsync(Guid id)
    {
        await _sender.Send(new DeleteItemQuery(id));
    }
}