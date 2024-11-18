using Application.Common;
using MediatR;

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
        await _repository.DeleteItemAsync(request.Id, cancellationToken);
    }
}