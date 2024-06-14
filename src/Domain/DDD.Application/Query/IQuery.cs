using MediatR;

namespace OpenModular.Domain.Application.Query;

public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}