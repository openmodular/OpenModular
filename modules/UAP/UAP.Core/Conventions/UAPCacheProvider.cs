using OpenModular.Cache.Abstractions;

namespace OpenModular.Module.UAP.Core.Conventions;

public class UAPCacheProvider() : CacheProviderAbstract(UAPConstants.ModuleCode);