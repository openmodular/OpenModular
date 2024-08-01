namespace OpenModular.DDD.Core.Domain.Entities;

/// <summary>
/// 软删除
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 是否删除
    /// </summary>
    bool IsDeleted { get; }
}