using Application.Common;
using Domain;
using MediatR;

namespace Application.Queries.GetItem;

public class GetItemQueryHandler : IRequestHandler<GetItemQuery, Item>
{
    private readonly IItemRepository _repository;

    public GetItemQueryHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetItemAsync(request.Id, cancellationToken);
    }
}