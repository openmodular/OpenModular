using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 定义一个查询请求
/// </summary>
/// <typeparam name="TDto"></typeparam>
public abstract class Query<TDto> : IQuery<TDto>
{
    public Guid QueryId { get; } = Guid.NewGuid();

    public AccountId? OperatorId { get; set; }
}