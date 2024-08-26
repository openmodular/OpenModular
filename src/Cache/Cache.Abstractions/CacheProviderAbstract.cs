using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

public abstract class CacheProviderAbstract : ICacheProvider
{
    protected CacheProviderAbstract( string name, IFusionCacheProvider cacheProvider)
    {
        ModuleCode = name;
        FusionCache = cacheProvider.GetCache(name);
    }

    public string ModuleCode { get; }

    public IFusionCache FusionCache { get; }

    public TValue GetOrSet<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        return FusionCache.GetOrSet(key, defaultValue, duration, cancellationToken);
    }

    public ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        return FusionCache.GetOrSetAsync(key, defaultValue, duration, cancellationToken);
    }

    public TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        return FusionCache.GetOrSet(key, factory, duration, cancellationToken);
    }

    public ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        return FusionCache.GetOrSetAsync(key, factory, duration, cancellationToken);
    }

    public MaybeValue<TValue> TryGet<TValue>(string key, CancellationToken cancellationToken = default)
    {
        return FusionCache.TryGet<TValue>(key, token: cancellationToken);
    }

    public ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, CancellationToken cancellationToken = default)
    {
        return FusionCache.TryGetAsync<TValue>(key, token: cancellationToken);
    }

    public void Set<TValue>(string key, TValue value, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        FusionCache.Set(key, value, duration, cancellationToken);
    }

    public ValueTask SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        return FusionCache.SetAsync(key, value, duration, cancellationToken);
    }

    public void Remove(string key, CancellationToken cancellationToken = default)
    {
        FusionCache.Remove(key, token: cancellationToken);
    }

    public ValueTask RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return FusionCache.RemoveAsync(key, token: cancellationToken);
    }

    public void Dispose()
    {
        FusionCache?.Dispose();
    }
}