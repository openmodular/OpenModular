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
    public string ModuleCode { get; private set; }

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; private set; }

    /// <summary>
    /// 值
    /// </summary>
    public string? Value { get; set; }

    public Config()
    {
        //fro ef
    }

    private Config(string moduleCode, string key, string? value) : base(new ConfigId())
    {
        Check.NotNull(moduleCode, nameof(moduleCode));
        Check.NotNull(key, nameof(key));

        ModuleCode = moduleCode;
        Key = key;
        Value = value;
    }

    public static Config Create(string moduleCode, string key, string? value)
    {
        return new Config(moduleCode, key, value);
    }
}