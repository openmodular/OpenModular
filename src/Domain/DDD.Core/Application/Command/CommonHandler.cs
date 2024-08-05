using MediatR;
using OpenModular.DDD.Core.Domain;
using OpenModular.Module.Abstractions.Exceptions;

namespace OpenModular.DDD.Core.Application.Command;

public abstract class CommandHandlerBase
{
    /// <summary>
    /// 检测业务规则
    /// </summary>
    /// <param name="rule"></param>
    /// <exception cref="ModuleBusinessException"></exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new ModuleBusinessException(rule.ModuleCode, rule.ErrorCode);
        }
    }
}

/// <summary>
/// 命令处理器基类
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public abstract class CommandHandler<TCommand> : CommandHandlerBase, ICommandHandler<TCommand> where TCommand : ICommand, IRequest
{
    public abstract Task Handle(TCommand request, CancellationToken cancellationToken);
}

/// <summary>
/// 命令处理器基类
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class CommandHandler<TCommand, TResult> : CommandHandlerBase, ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>, IRequest<TResult>
{
    public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
}