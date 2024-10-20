using MediatR;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 定义一个查询请求
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IQuery<out TResult> : IRequest<TResult>
{
    /// <summary>
    /// 查询标识
    /// </summary>
    Guid QueryId { get; }

    /// <summary>
    /// 操作人标识
    /// </summary>
    AccountId? OperatorId { get; set; }
}