using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 缓存提供器
/// </summary>
public interface ICacheProvider : IDisposable
{
    /// <summary>
    /// 缓存提供器类型
    /// </summary>
    CacheProviderType Type { get; }

    /// <summary>
    /// 缓存提供器名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// A FusionCache instance
    /// </summary>
    IFusionCache FusionCache { get; }

    #region ==GetOrSet==

    /// <summary>
    /// 从缓存中获取指定键的值，如果不存在则设置默认值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    TValue GetOrSet<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 从缓存中获取指定键的值，如果不存在则设置默认值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 从缓存中获取指定键的值，如果不存在则执行委托并将其结果添加到缓存
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="factory">缓存不存在时执行的委托函数</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    TValue GetOrSet<TValue>(string key, Func<CancellationToken, TValue> factory, TimeSpan duration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 从缓存中获取指定键的值，如果不存在则执行委托并将其结果添加到缓存
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="factory">缓存不存在时执行的委托函数</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<CancellationToken, Task<TValue>> factory, TimeSpan duration, CancellationToken cancellationToken = default);

    #endregion

    #region ==TryGet==

    /// <summary>
    /// 尝试从缓存中获取指定键的值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    MaybeValue<TValue> TryGet<TValue>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 尝试从缓存中获取指定键的值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, CancellationToken cancellationToken = default);

    #endregion

    #region ==Set==

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">值</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    void Set<TValue>(string key, TValue value, TimeSpan duration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">值</param>
    /// <param name="duration">缓存有效期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    ValueTask SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken cancellationToken = default);

    #endregion

    #region ==Remove==

    /// <summary>
    /// 从缓存中删除指定键的值
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    void Remove(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 从缓存中删除指定键的值
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    ValueTask RemoveAsync(string key, CancellationToken cancellationToken = default);

    #endregion
}