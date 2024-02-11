using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS.Commands
{
    public interface ICommand<TCommandResult> : IRequest<TCommandResult>
    {

    }
}
