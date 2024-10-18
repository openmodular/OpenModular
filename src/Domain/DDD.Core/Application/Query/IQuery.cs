using MediatR;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 定义一个查询请求
/// </summary>
/// <typeparam name="TResult"></typeparam>
public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid QueryId { get; }
}