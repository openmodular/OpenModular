using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

public interface ICache : IFusionCache
{
    string ModuleCode { get; }

    IFusionCache FusionCache { get; }

    #region GetOrSet overloads (with default value)

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    TValue GetOrSet<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">In case the value is not in the cache this value will be saved and returned instead.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    TValue GetOrSet<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrSet overloads (with factory and fail-safe default value)

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="IFusionCache.DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="IFusionCache.DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, FusionCacheEntryOptions? options = null, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="failSafeDefaultValue">In case fail-safe is activated and there's no stale data to use, this value will be used instead of throwing an exception.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrSet overloads (with factory)

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="IFusionCache.DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, FusionCacheEntryOptions? options, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="options"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="options">The options to adhere during this operation. If null is passed, <see cref="IFusionCache.DefaultEntryOptions"/> will be used.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, FusionCacheEntryOptions? options, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <paramref name="duration"/> provided.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="factory"/> will be called and the returned value saved according with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="factory">The function which will be called if the value is not found in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache, either already there or generated using the provided <paramref name="factory"/> .</returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region GetOrDefault

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, TValue? defaultValue = default, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    TValue? GetOrDefault<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, TValue? defaultValue = default, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, TValue? defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/>: if not there, the <paramref name="defaultValue"/> will be returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="defaultValue">The default value to return if the value for the given <paramref name="key"/> is not in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>The value in the cache or the <paramref name="defaultValue"/> .</returns>
    TValue? GetOrDefault<TValue>(string key, TValue? defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region TryGet

    /// <summary>
    /// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="MaybeValue{TValue}"/> instance.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Try to get the value of type <typeparamref name="TValue"/> in the cache for the specified <paramref name="key"/> and returns a <see cref="MaybeValue{TValue}"/> instance.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    MaybeValue<TValue> TryGet<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region Set 

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
    ValueTask SetAsync<TValue>(string key, TValue value, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="duration"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
    ValueTask SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Set<TValue>(string key, TValue value, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the provided <paramref name="duration"/>. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="duration">The value for the newly created <see cref="FusionCacheEntryOptions.Duration"/> property, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Set<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
    ValueTask SetAsync<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Put the <paramref name="value"/> in the cache for the specified <paramref name="key"/> with the <see cref="FusionCacheEntryOptions"/> resulting by calling the provided <paramref name="setupAction"/> lambda. If a value is already there it will be overwritten.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the cache.</typeparam>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="value">The value to put in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Set<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region Remove

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask RemoveAsync(string key, CancellationToken token);

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> to await the completion of the operation.</returns>
    ValueTask RemoveAsync(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Remove(string key, CancellationToken token);

    /// <summary>
    /// Removes the value in the cache for the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The cache key which identifies the entry in the cache.</param>
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Remove(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion

    #region Expire

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
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask ExpireAsync(string key, CancellationToken token = default);

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
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    ValueTask ExpireAsync(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

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
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask"/> to await the completion of the operation.</returns>
    void Expire(string key, CancellationToken token = default);

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
    /// <param name="setupAction">The setup action used to further configure the newly created <see cref="FusionCacheEntryOptions"/> object, automatically created by duplicating <see cref="IFusionCache.DefaultEntryOptions"/>.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the operation.</param>
    void Expire(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default);

    #endregion
}