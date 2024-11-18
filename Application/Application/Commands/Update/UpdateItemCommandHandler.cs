using Application.Common;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Update;
public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand,Item>
{
    private readonly IItemRepository _repository;
    public UpdateItemCommandHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetItemAsync(request.ItemId);
        if (item == null)
        {
            throw new ArgumentNullException(nameof(request));
        }        
        item.Name = request.Name ?? item.Name;
        item.ChildItems = request.ChildItems ?? item.ChildItems;
        item.ParentId = request.ParentId ?? item.ParentId;

        foreach(var child in item.ChildItems)
        {
            child.Id = Guid.NewGuid();
            child.ParentId = item.Id;
        }

        await _repository.UpdateItemAsync(item);
        return item;
    }
}
