using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Delete;
public class DeleteItemQueryHandler : IRequestHandler<DeleteItemQuery>
{
    private readonly IItemRepository _repository;

    public DeleteItemQueryHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteItemQuery request, CancellationToken cancellationToken)
    {
        await _repository.DeleteItemAsync(request.Id);
    }
}
