using MediatR;

namespace BuildingBlocks.CQRS.Commands.Dispatcher;
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IMediator mediator;

    public CommandDispatcher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return mediator.Send(command, cancellationToken);
    }
}