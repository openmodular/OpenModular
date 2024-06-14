using OpenModular.Common.Utils;

namespace OpenModular.DDD.Core.Domain.Exceptions;

//从 https://github.com/abpframework/abp/blob/7.3.0/framework/src/Volo.Abp.ExceptionHandling/Volo/Abp/Domain/Entities/EntityNotFoundException.cs 复制

/// <summary>
/// 实体未发现时抛出的异常
/// </summary>
public class EntityNotFoundException : ExceptionBase
{
    /// <summary>
    /// 实体类型
    /// </summary>
    public Type? EntityType { get; set; }

    /// <summary>
    /// 实体的唯一序号
    /// </summary>
    public object? Id { get; set; }

    /// <summary>
    /// 创建一个新的 <see cref="EntityNotFoundException"/> 对象
    /// </summary>
    public EntityNotFoundException()
    {

    }

    /// <summary>
    /// 创建一个新的 <see cref="EntityNotFoundException"/> 对象
    /// </summary>
    /// <param name="entityType">实体类型</param>
    public EntityNotFoundException(Type entityType) : this(entityType, null, null)
    {

    }

    /// <summary>
    /// 创建一个新的 <see cref="EntityNotFoundException"/> 对象
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="id">实体标识</param>
    public EntityNotFoundException(Type entityType, object? id) : this(entityType, id, null)
    {

    }

    /// <summary>
    /// 创建一个新的 <see cref="EntityNotFoundException"/> 对象
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="id">实体标识</param>
    /// <param name="innerException">内部异常</param>
    public EntityNotFoundException(Type entityType, object? id, Exception? innerException)
        : base(
            id == null
                ? $"There is no such an entity given id. Entity type: {entityType.FullName}"
                : $"There is no such an entity. Entity type: {entityType.FullName}, id: {id}",
            innerException)
    {
        EntityType = entityType;
        Id = id;
    }

    /// <summary>
    /// Creates a new <see cref="EntityNotFoundException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    public EntityNotFoundException(string message) : base(message)
    {

    }

    /// <summary>
    /// Creates a new <see cref="EntityNotFoundException"/> object.
    /// </summary>
    /// <param name="message">Exception message</param>
    /// <param name="innerException">Inner exception</param>
    public EntityNotFoundException(string message, Exception? innerException) : base(message, innerException)
    {

    }
}
