using MediatR;

namespace OpenModular.DDD.Core.Application.Command;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand, IRequest
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>, IRequest<TResult>
{
}