namespace OpenModular.Config.Abstractions;

/// <summary>
/// 配置描述符
/// </summary>
public interface IConfigDescriptor
{
    /// <summary>
    /// 模块编码
    /// </summary>
    string ModuleCode { get; }

    /// <summary>
    /// 配置类的类型
    /// </summary>
    Type ConfigType { get; }

    /// <summary>
    /// 配置键列表
    /// </summary>
    List<ConfigKey> Keys { get; }
}