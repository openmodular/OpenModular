using OpenModular.Cache.Abstractions;
using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// UAP缓存提供者
/// </summary>
/// <param name="cacheProvider"></param>
public class UAPCacheProvider(IFusionCacheProvider cacheProvider) : CacheProviderAbstract(UAPConstants.ModuleCode, cacheProvider);