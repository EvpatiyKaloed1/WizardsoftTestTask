using Domain;
using MediatR;

namespace Application.Commands.Create;
public record CreateItemCommand(string Name, List<Item> ChildItems) : IRequest<Item>;