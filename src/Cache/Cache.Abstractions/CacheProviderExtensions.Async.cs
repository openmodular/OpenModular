using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// ICacheProvider 异步扩展方法
/// </summary>
public static partial class CacheProviderExtensions
{
    public static ValueTask<TValue> GetOrSetAsync<TValue>(this ICacheProvider provider, string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue = default,
        FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return provider.FusionCache.GetOrSetAsync(key, factory, failSafeDefaultValue, options, token);
    }

    public static ValueTask<TValue> GetOrSetAsync<TValue>(this ICacheProvider provider, string key, TValue defaultValue, FusionCacheEntryOptions options = null,
        CancellationToken token = default)
    {
        return provider.FusionCache.GetOrSetAsync(key, defaultValue, options, token);
    }

    public static ValueTask<TValue> GetOrDefaultAsync<TValue>(this ICacheProvider provider, string key, TValue defaultValue = default, FusionCacheEntryOptions options = null,
        CancellationToken token = default)
    {
        return provider.FusionCache.GetOrDefaultAsync<TValue>(key, defaultValue, options, token);
    }

    public static ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(this ICacheProvider provider, string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return provider.FusionCache.TryGetAsync<TValue>(key, options, token);
    }

    public static ValueTask SetAsync<TValue>(this ICacheProvider provider, string key, TValue value, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return provider.FusionCache.SetAsync(key, value, options, token);
    }

    public static ValueTask RemoveAsync(this ICacheProvider provider, string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return provider.FusionCache.RemoveAsync(key, options, token);
    }

    public static ValueTask ExpireAsync(this ICacheProvider provider, string key, FusionCacheEntryOptions options = null, CancellationToken token = default)
    {
        return provider.FusionCache.ExpireAsync(key, options, token);
    }
}