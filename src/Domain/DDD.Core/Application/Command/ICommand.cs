using MediatR;

namespace OpenModular.DDD.Core.Application.Command;

public interface ICommand<out TResult> : IRequest<TResult>
{
    Guid CommandId { get; }
}

public interface ICommand : IRequest
{
    Guid CommandId { get; }
}