using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

/// <summary>
/// 配置信息
/// </summary>
public class Config : AggregateRoot<ConfigId>
{
    /// <summary>
    /// 模块编码
    /// </summary>
    public string ModuleCode { get; }

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    public Config()
    {

    }

    private Config(string moduleCode, string key, string value)
    {
        Check.NotNull(moduleCode, nameof(moduleCode));
        Check.NotNull(key, nameof(key));

        ModuleCode = moduleCode;
        Key = key;
        Value = value;
    }

    public static Config Create(string moduleCode, string key, string value)
    {
        return new Config(moduleCode, key, value);
    }
}