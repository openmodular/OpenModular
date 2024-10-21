namespace OpenModular.DDD.Core.Domain.Entities;

/// <summary>
/// 软删除
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 已删除
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    DateTimeOffset? DeletedAt { get; set; }
}