using MediatR;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.DDD.Core.Application.Command;

public interface ICommand<out TResult> : IRequest<TResult>
{
    /// <summary>
    /// 命令唯一标识
    /// </summary>
    Guid CommandId { get; }

    /// <summary>
    /// 操作人标识
    /// </summary>
    AccountId? OperatorId { get; set; }
}

public interface ICommand : IRequest
{
    /// <summary>
    /// 命令唯一标识
    /// </summary>
    Guid CommandId { get; }

    /// <summary>
    /// 操作人标识
    /// </summary>
    AccountId? OperatorId { get; set; }
}