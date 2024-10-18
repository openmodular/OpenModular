using MediatR;
using OpenModular.Common.Utils;
using OpenModular.DDD.Core.Domain;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.Abstractions.Exceptions;

namespace OpenModular.DDD.Core.Application.Command;

public abstract class HandlerBase
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
public abstract class CommandHandler<TCommand> : HandlerBase, ICommandHandler<TCommand> where TCommand : ICommand, IRequest
{
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        await ExecuteAsync(request, cancellationToken);
    }

    public abstract Task ExecuteAsync(TCommand request, CancellationToken cancellationToken);
}

/// <summary>
/// 命令处理器基类
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class CommandHandler<TCommand, TResult> : HandlerBase, ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var result = await ExecuteAsync(request, cancellationToken);
        return result;
    }

    public abstract Task<TResult> ExecuteAsync(TCommand request, CancellationToken cancellationToken);
}