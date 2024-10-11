using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.DDD.Core.Domain.Entities;

/// <summary>
/// 继承该接口实现多租户功能
/// </summary>
public interface ITenant
{
    /// <summary>
    /// 租户标识
    /// </summary>
    TenantId TenantId { get; set; }
}