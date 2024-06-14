namespace OpenModular.DDD.Core.Domain.Entities;

/// <summary>
/// 无唯一标识的实体接口
/// </summary>
public interface IEntity;

/// <summary>
/// 有唯一标识的实体接口
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<out TKey> : IEntity
{
    /// <summary>
    /// 实体唯一标识
    /// </summary>
    TKey Id { get; }
}