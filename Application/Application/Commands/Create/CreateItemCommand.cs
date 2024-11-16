using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Create;
public record CreateItemCommand (string Name,  List<Item> ChildItems) :IRequest;

