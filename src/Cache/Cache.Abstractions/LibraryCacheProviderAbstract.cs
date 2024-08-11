using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 类库缓存提供器抽象类
/// </summary>
public abstract class LibraryCacheProviderAbstract : CacheProviderAbstract
{
    protected LibraryCacheProviderAbstract(string name, IFusionCacheProvider cacheProvider) : base(CacheProviderType.Library, name, cacheProvider)
    {
    }
}