using MediatR;

namespace BuildingBlocks.CQRS.Queris.Dispatcher
{
    internal class QueryDispatcher : IQueryDispatcher
    {

        private readonly IMediator _mediator;

        public QueryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(query, cancellationToken);
        }
    }
}
