using Domain;
using MediatR;

namespace Application.Commands.Update;
public record UpdateItemCommand(Guid? ParentId,Guid? ItemId, string Name, List<Item> ChildItems) : IRequest<Item>;
