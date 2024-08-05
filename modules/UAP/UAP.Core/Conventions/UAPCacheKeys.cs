using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// 统一认证平台缓存键
/// </summary>
public class UAPCacheKeys
{
    public string User(UserId userId) => $"{UAPConstants.ModuleCode}:User:{userId}";
}