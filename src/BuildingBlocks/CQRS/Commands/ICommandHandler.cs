using MediatR;

namespace BuildingBlocks.CQRS.Commands;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : class, ICommand<TResult>
{

}
