using OpenModular.Cache.Abstractions;
using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.FusionCache;

internal class FusionCacheCacheProvider(IFusionCacheProvider cacheProvider) : ICacheProvider
{
    public string Get()
    {
        var cache = cacheProvider.GetCache("");

        var product = cache.GetOrSet<Product>(
            $"product:{id}",
            _ => GetProductFromDb(id),
            TimeSpan.FromSeconds(30)
        );
    }
}