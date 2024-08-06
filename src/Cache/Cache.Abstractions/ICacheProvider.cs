using ZiggyCreatures.Caching.Fusion;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 缓存提供器
/// </summary>
public interface ICacheProvider
{
    /// <summary>
    /// 模块编码
    /// </summary>
    string ModuleCode { get; }

    /// <summary>
    /// A FusionCache instance
    /// </summary>
    IFusionCache FusionCache { get; }
}