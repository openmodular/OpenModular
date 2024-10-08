using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.Web;

/// <summary>
/// 租户解析器
/// </summary>
public interface ITenantResolver
{
    /// <summary>
    /// 解析租户编号
    /// </summary>
    /// <returns></returns>
    Task<TenantId> ResolveAsync();
}

internal class TenantResolver : ITenantResolver
{
    public Task<TenantId> ResolveAsync()
    {
        return Task.FromResult(default(TenantId));
    }
}