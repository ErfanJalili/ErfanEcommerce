using MediatR;

namespace BuildingBlocks.CQRS.Queris;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : class, IQuery<TResult>
{
}
