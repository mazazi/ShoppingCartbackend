using Tatweer.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatweer.Application.Commands.Products
{
    public record MarkProductAsVisibleCommand(int Id) : IRequest<Result>;
}
