using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions
{
    public abstract class CacheProviderAbstract : ICacheProvider
    {
        protected CacheProviderAbstract(string moduleCode, IFusionCacheProvider cacheProvider)
        {
            ModuleCode = moduleCode;
            FusionCache = cacheProvider.GetCache(moduleCode);
        }

        public string ModuleCode { get; }

        public IFusionCache FusionCache { get; }
    }
}
