using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 模块缓存提供器抽象类
/// </summary>
public abstract class ModuleCacheProviderAbstract : CacheProviderAbstract
{
    protected ModuleCacheProviderAbstract(string moduleCode, IFusionCacheProvider cacheProvider) : base(CacheProviderType.Module, moduleCode, cacheProvider)
    {
    }
}