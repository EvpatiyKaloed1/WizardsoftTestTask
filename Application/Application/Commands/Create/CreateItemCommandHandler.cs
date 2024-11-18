using Application.Common;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand,Item>
{
    private readonly IItemRepository _repository;

    public CreateItemCommandHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Item(request.Name, request.ChildItems);
        await _repository.CreateItemAsync(item);
        return item;
    }
}
