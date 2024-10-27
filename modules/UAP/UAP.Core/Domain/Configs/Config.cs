using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

/// <summary>
/// 配置信息
/// </summary>
public class Config : AggregateRoot<ConfigId>
{
    private string _moduleCode;

    /// <summary>
    /// 模块编码
    /// </summary>
    public string ModuleCode
    {
        get => _moduleCode;
        set
        {
            Check.NotNull(value, nameof(ModuleCode));
            _moduleCode = value;
        }
    }

    /// <summary>
    /// 值
    /// </summary>
    public string? Value { get; set; }

    public Config()
    {
        //fro ef
    }

    private Config(string moduleCode, string? value) : base(new ConfigId())
    {
        ModuleCode = moduleCode;
        Value = value;
    }

    public static Config Create(string moduleCode, string? value)
    {
        return new Config(moduleCode, value);
    }
}