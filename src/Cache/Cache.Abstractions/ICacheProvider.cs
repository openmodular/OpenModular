using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 缓存提供器
/// </summary>
public interface ICacheProvider<out TCacheKeys> where TCacheKeys : ICacheKeys
{
    TCacheKeys CacheKeys { get; }

    /// <summary>
	/// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according to the <paramref name="options"/> provided.
	/// </summary>
	/// <typeparam name="TValue">The type of the value in the cache.</typeparam>
	/// <param name="key">The cache key which identifies the entry in the cache.</param>
	/// <param name="factory">The function which will be called if the value is not found in the cache.</param>
	/// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
	/// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
	/// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
	/// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
	ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue = default, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according to the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue = default, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according to the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according to the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    TValue GetOrSet<TValue>(string key, TValue defaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, TValue? defaultValue = default, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    TValue? GetOrDefault<TValue>(string key, TValue? defaultValue = default, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="MaybeValue{TValue}"/> instance.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="MaybeValue{TValue}"/> instance.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    MaybeValue<TValue> TryGet<TValue>(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="options"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask SetAsync<TValue>(string key, TValue value, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="options"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Set<TValue>(string key, TValue value, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask RemoveAsync(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Remove(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Expires the cache entry for the specified <paramref name="key"/>.
    /// <br/>
    /// <br/>
    /// In the memory cache:
    /// <br/>
    /// - if fail-safe is enabled: the entry will marked as logically expired, but will still be available as a fallback value in case of future problems
    /// <br/>
    /// - if fail-safe is disabled: the entry will be effectively removed
    /// <br/>
    /// <br/>
    /// In the distributed cache (if any), the entry will be effectively removed.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask ExpireAsync(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Expires the cache entry for the specified <paramref name="key"/>.
    /// <br/>
    /// <br/>
    /// In the memory cache:
    /// <br/>
    /// - if fail-safe is enabled: the entry will marked as logically expired, but will still be available as a fallback value in case of future problems
    /// <br/>
    /// - if fail-safe is disabled: the entry will be effectively removed
    /// <br/>
    /// <br/>
    /// In the distributed cache (if any), the entry will be effectively removed.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Expire(string key, FusionCacheEntryOptions? options = null, CancellationToken token = default);
}