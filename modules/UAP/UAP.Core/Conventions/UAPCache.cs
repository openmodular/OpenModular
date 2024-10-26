using OpenModular.Cache.Abstractions;
using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Module.UAP.Core.Conventions;

/// <summary>
/// UAP缓存器
/// </summary>
public class UAPCache(IFusionCacheProvider provider) : CacheAbstract(UAPConstants.ModuleCode, provider);