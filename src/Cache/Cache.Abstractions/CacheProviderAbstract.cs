using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Locking;

namespace OpenModular.Cache.Abstractions
{
    public abstract class CacheProviderAbstract<TCacheKeys> : FusionCache, ICacheProvider<TCacheKeys> where TCacheKeys : ICacheKeys
    {
        protected CacheProviderAbstract(TCacheKeys cacheKeys, IOptions<FusionCacheOptions> optionsAccessor, IMemoryCache? memoryCache = null, ILogger<FusionCache>? logger = null, IFusionCacheMemoryLocker? memoryLocker = null) : base(optionsAccessor, memoryCache, logger, memoryLocker)
        {
            CacheKeys = cacheKeys;
        }

        public TCacheKeys CacheKeys { get; }
    }
}
