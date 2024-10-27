using OpenModular.Common.Utils;

namespace OpenModular.Cache.Abstractions;

/// <summary>
/// 缓存配置项
/// </summary>
public class CacheOptions
{
    public const string Position = $"{OpenModularConstants.Name}:Cache";

    /// <summary>
    /// 自定义缓存配置，当需要自定义缓存配置逻辑时，需要将该配置设置为true
    /// </summary>
    public bool Custom { get; set; }

    /// <summary>
    /// 缓存模式
    /// </summary>
    public CacheMode Mode { get; set; }

    /// <summary>
    /// Redis配置
    /// </summary>
    public RedisOptions? Redis { get; set; }
}

/// <summary>
/// 缓存模式
/// </summary>
public enum CacheMode
{
    /// <summary>
    /// 内存模式
    /// </summary>
    Memory,
    /// <summary>
    /// Redis模式
    /// </summary>
    Redis
}

/// <summary>
/// Redis配置项
/// </summary>
public class RedisOptions
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// 实例名称
    /// </summary>
    public string? InstanceName { get; set; }

    /// <summary>
    /// 开启背板功能
    /// </summary>
    public bool EnableBackplane { get; set; }
}