using Microsoft.Extensions.Caching.Distributed;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane;
using ZiggyCreatures.Caching.Fusion.Events;
using ZiggyCreatures.Caching.Fusion.Plugins;
using ZiggyCreatures.Caching.Fusion.Serialization;

namespace OpenModular.Cache.Abstractions
{
    public abstract partial class CacheAbstract : ICache
    {
        protected CacheAbstract(string moduleCode, IFusionCacheProvider cacheProvider)
        {
            ModuleCode = moduleCode;
            FusionCache = cacheProvider.GetCache(moduleCode);
        }

        public string CacheName => FusionCache.CacheName;

        public string InstanceId => FusionCache.InstanceId;

        public FusionCacheEntryOptions DefaultEntryOptions => FusionCache.DefaultEntryOptions;

        public bool HasDistributedCache => FusionCache.HasDistributedCache;

        public bool HasBackplane => FusionCache.HasBackplane;

        public FusionCacheEventsHub Events => FusionCache.Events;

        public string ModuleCode { get; }

        public IFusionCache FusionCache { get; }

        public FusionCacheEntryOptions CreateEntryOptions(Action<FusionCacheEntryOptions>? setupAction = null, TimeSpan? duration = null)
        {
            return FusionCache.CreateEntryOptions(setupAction, duration);
        }

        public ValueTask<TValue> GetOrSetAsync<TValue>(string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, Task<TValue>> factory, MaybeValue<TValue> failSafeDefaultValue = new(),
            FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            return FusionCache.GetOrSetAsync(key, factory, failSafeDefaultValue, options, token);
        }

        public TValue GetOrSet<TValue>(string key, Func<FusionCacheFactoryExecutionContext<TValue>, CancellationToken, TValue> factory, MaybeValue<TValue> failSafeDefaultValue = new(),
            FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            return FusionCache.GetOrSet(key, factory, failSafeDefaultValue, options, token);
        }

        public ValueTask<TValue> GetOrSetAsync<TValue>(string key, TValue defaultValue, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.GetOrSetAsync(key, defaultValue, options, token);
        }

        public TValue GetOrSet<TValue>(string key, TValue defaultValue, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.GetOrSet(key, defaultValue, options, token);
        }

        public ValueTask<TValue?> GetOrDefaultAsync<TValue>(string key, TValue? defaultValue = default(TValue?),
            FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            return FusionCache.GetOrDefaultAsync(key, defaultValue, options, token);
        }

        public TValue? GetOrDefault<TValue>(string key, TValue? defaultValue = default(TValue?),
            FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            return FusionCache.GetOrDefault(key, defaultValue, options, token);
        }

        public ValueTask<MaybeValue<TValue>> TryGetAsync<TValue>(string key, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.TryGetAsync<TValue>(key, options, token);
        }

        public MaybeValue<TValue> TryGet<TValue>(string key, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.TryGet<TValue>(key, options, token);
        }

        public ValueTask SetAsync<TValue>(string key, TValue value, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.SetAsync(key, value, options, token);
        }

        public void Set<TValue>(string key, TValue value, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            FusionCache.Set(key, value, options, token);
        }

        public ValueTask RemoveAsync(string key, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.RemoveAsync(key, options, token);
        }

        public void Remove(string key, FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            FusionCache.Remove(key, options, token);
        }

        public ValueTask ExpireAsync(string key, FusionCacheEntryOptions? options = null,
            CancellationToken token = new())
        {
            return FusionCache.ExpireAsync(key, options, token);
        }

        public void Expire(string key, FusionCacheEntryOptions? options = null, CancellationToken token = new())
        {
            FusionCache.Expire(key, options, token);
        }

        public IFusionCache SetupSerializer(IFusionCacheSerializer serializer)
        {
            return FusionCache.SetupSerializer(serializer);
        }

        public IFusionCache SetupDistributedCache(IDistributedCache distributedCache)
        {
            return FusionCache.SetupDistributedCache(distributedCache);
        }

        public IFusionCache SetupDistributedCache(IDistributedCache distributedCache, IFusionCacheSerializer serializer)
        {
            return FusionCache.SetupDistributedCache(distributedCache, serializer);
        }

        public IFusionCache RemoveDistributedCache()
        {
            return FusionCache.RemoveDistributedCache();
        }

        public IFusionCache SetupBackplane(IFusionCacheBackplane backplane)
        {
            return FusionCache.SetupBackplane(backplane);
        }

        public IFusionCache RemoveBackplane()
        {
            return FusionCache.RemoveBackplane();
        }

        public void AddPlugin(IFusionCachePlugin plugin)
        {
            FusionCache.AddPlugin(plugin);
        }

        public bool RemovePlugin(IFusionCachePlugin plugin)
        {
            return FusionCache.RemovePlugin(plugin);
        }

        public void Dispose()
        {
            FusionCache.Dispose();
        }
    }
}