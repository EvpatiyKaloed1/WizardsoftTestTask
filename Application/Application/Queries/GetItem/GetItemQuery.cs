using Domain;
using MediatR;

namespace Application.Queries.GetItem;
public record GetItemQuery(Guid Id) : IRequest<Item>;