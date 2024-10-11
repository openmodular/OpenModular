using MediatR;

namespace OpenModular.DDD.Core.Application.Query;

public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid QueryId { get; }
}