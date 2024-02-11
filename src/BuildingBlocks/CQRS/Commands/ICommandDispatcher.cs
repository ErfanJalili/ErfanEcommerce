namespace BuildingBlocks.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);/* where TCommand : class, ICommand<TResult>*/
    }
}
