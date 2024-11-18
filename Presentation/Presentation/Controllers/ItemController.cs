using Application.Commands.Create;
using Application.Commands.Update;
using Application.Queries.Delete;
using Application.Queries.GetItem;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using Swashbuckle.AspNetCore.Annotations;

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
    public async Task<Item> GetItem(Guid id, CancellationToken token)
    {
        return await _sender.Send(new GetItemQuery(id), token);
    }

    #region Json

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

    #endregion Json

    [HttpPost("create")]
    public async Task<Item> CreateItemAsync([FromBody] ItemDto dto, CancellationToken token)
    {
        var item = dto.MapToItem();

        return await _sender.Send(new CreateItemCommand(item.Name, item.ChildItems), token);
    }

    [HttpPut("update")]
    public async Task<Item> UpdateItemAsync([FromBody] UpdateItemDto dto, CancellationToken token)
    {
        var item = dto.MapToItem();

        return await _sender.Send(new UpdateItemCommand(item.ParentId, item.Id, item.Name, item.ChildItems), token);
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task DeleteItemAsync(Guid id, CancellationToken token)
    {
        await _sender.Send(new DeleteItemQuery(id), token);
    }
}