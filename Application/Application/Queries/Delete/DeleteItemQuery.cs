using MediatR;

namespace Application.Queries.Delete;
public record DeleteItemQuery(Guid Id) : IRequest;