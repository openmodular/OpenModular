using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions
{
    public abstract partial class CacheAbstract
    {
        #region GetOrSet overloads (with default value)

        public ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default)
        {
            return FusionCache.GetOrSetAsync(key, defaultValue, duration, token);
        }

        public TValue GetOrSet<TValue>(string key, TValue defaultValue, TimeSpan duration, CancellationToken token = default)
        {
            return FusionCache.GetOrSet(key, defaultValue, duration, token);
        }

        public ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.GetOrSetAsync(key, defaultValue, setupAction, token);
        }

        public TValue GetOrSet<TValue>(string key, TValue defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.GetOrSet(key, defaultValue, setupAction, token);
        }

        #endregion

        #region GetOrDefault overloads

        public ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, TValue? defaultValue = default, CancellationToken token = default)
        {
            return FusionCache.GetOrDefaultAsync(key, defaultValue, setupAction, token);
        }

        public TValue? GetOrDefault<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, TValue? defaultValue = default, CancellationToken token = default)
        {
            return FusionCache.GetOrDefault(key, defaultValue, setupAction, token);
        }

        public ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, TValue? defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.GetOrDefaultAsync(key, defaultValue, setupAction, token);
        }

        public TValue? GetOrDefault<TValue>(string key, TValue? defaultValue, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.GetOrDefault(key, defaultValue, setupAction, token);
        }

        #endregion

        #region TryGet overloads

        public ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.TryGetAsync<TValue>(key, setupAction, token);
        }

        public MaybeValue<TValue> TryGet<TValue>(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.TryGet<TValue>(key, setupAction, token);
        }

        #endregion

        #region Set overloads

        public ValueTask SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default)
        {
            return FusionCache.SetAsync(key, value, duration, token);
        }

        public void Set<TValue>(string key, TValue value, TimeSpan duration, CancellationToken token = default)
        {
            FusionCache.Set(key, value, duration, token);
        }

        public ValueTask SetAsync<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.SetAsync(key, value, setupAction, token);
        }

        public void Set<TValue>(string key, TValue value, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            FusionCache.Set(key, value, setupAction, token);
        }

        #endregion

        #region Remove overloads

        public ValueTask RemoveAsync(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.RemoveAsync(key, setupAction, token);
        }

        public void Remove(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            FusionCache.Remove(key, setupAction, token);
        }

        #endregion

        #region Expire overloads

        public ValueTask ExpireAsync(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            return FusionCache.ExpireAsync(key, setupAction, token);
        }

        public void Expire(string key, Action<FusionCacheEntryOptions> setupAction, CancellationToken token = default)
        {
            FusionCache.Expire(key, setupAction, token);
        }

        #endregion
    }
}