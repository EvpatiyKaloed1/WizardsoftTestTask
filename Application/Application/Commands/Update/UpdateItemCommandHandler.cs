using Application.Common;
using Domain;
using MediatR;

namespace Application.Commands.Update;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Item>
{
    private readonly IItemRepository _repository;

    public UpdateItemCommandHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetItemAsync(request.ItemId, cancellationToken)
            ?? throw new InvalidOperationException($"Item with id: {request.ItemId} not found");

        item.Update(request.Name, request.ChildItems, request.ParentId);

        await _repository.UpdateItemAsync(item, cancellationToken);

        return item;
    }
}