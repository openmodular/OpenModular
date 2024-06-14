namespace OpenModular.DDD.Domain.Entities;

/// <summary>
/// 无唯一标识的聚合根接口
/// </summary>
public interface IAggregateRoot : IEntity;

/// <summary>
/// 有唯一标识的聚合根接口
/// </summary>
/// <typeparam name="TKey">唯一标识的类型</typeparam>
public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot;