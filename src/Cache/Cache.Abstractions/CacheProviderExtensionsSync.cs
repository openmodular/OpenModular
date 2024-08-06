using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// ICacheProvider 扩展方法
/// </summary>
public static partial class CacheProviderExtensions
{
    public TValue GetOrSet<TValue>(string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue = default,
        FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return FusionCache.GetOrSet(key, factory, failSafeDefaultValue, options, token);
    }


    public TValue GetOrSet<TValue>(string key, TValue defaultValue, FusionCacheEntryOptions options = null,
        CancellationToken token = default)
    {
        return FusionCache.GetOrSet(key, defaultValue, options, token);
    }

    public TValue GetOrDefault<TValue>(string key, TValue defaultValue = default, FusionCacheEntryOptions options = null,
        CancellationToken token = default)
    {
        return FusionCache.GetOrDefault(key, defaultValue, options, token);
    }

    public MaybeValue<TValue> TryGet<TValue>(string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return FusionCache.TryGet<TValue>(key, options, token);
    }

    public void Set<TValue>(string key, TValue value, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        FusionCache.Set(key, value, options, token);
    }

    public void Remove(string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        FusionCache.Remove(key, options, token);
    }

    public void Expire(string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        FusionCache.Expire(key, options, token);
    }
}