using Application.Common;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
{
    private readonly IItemRepository _repository;

    public CreateItemCommandHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
      
        await _repository.CreateItem(new Item(request.Name, request.ChildItems ));
    }


    private async Task Validate(Guid? parentId)
    {
        var item = await _repository.GetItemAsync(parentId);
        if (item == null)
        {
            throw new ItemExeption();
        }
    }
}
