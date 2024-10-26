using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

public interface ICache : IFusionCache
{
    string ModuleCode { get; }

    IFusionCache FusionCache { get; }

    #region GetOrSet

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrSet overloads (with factory and fail-safe default value)

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, TimeSpan duration, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, TimeSpan duration, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrSet overloads (with factory)

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, FusionCacheEntryOptions? options, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, FusionCacheEntryOptions? options, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, TimeSpan duration, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, TimeSpan duration, CancellationToken token = default);

    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrDefault

    ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction,
        TValue? defaultValue = default, CancellationToken token = default);

    TValue? GetOrDefault<TValue>(string key, Action<FusionCacheEntryOptions> setupAction,
        TValue? defaultValue = default, CancellationToken token = default);

    ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, TValue? defaultValue,
        Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    TValue? GetOrDefault<TValue>(string key, TValue? defaultValue, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    #endregion

    #region TryGet 

    ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    MaybeValue<TValue> TryGet<TValue>(string key, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    #endregion

    #region Set 

    ValueTask SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default);

    void Set<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default);

    ValueTask SetAsync<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    void Set<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    #endregion

    #region Remove

    ValueTask RemoveAsync(string key, CancellationToken token);

    ValueTask RemoveAsync(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    void Remove(string key, CancellationToken token);

    void Remove(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region Expire

    ValueTask ExpireAsync(string key, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    void Expire(string key, Action<FusionCacheEntryOptions> setupAction,
        CancellationToken token = default);

    #endregion
}