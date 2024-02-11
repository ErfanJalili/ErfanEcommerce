using MediatR;

namespace BuildingBlocks.CQRS.Queris;

public interface IQuery<TResult> : IRequest<TResult>
{
}
