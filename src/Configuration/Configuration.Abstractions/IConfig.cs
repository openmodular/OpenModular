namespace OpenModular.Configuration.Abstractions;

/// <summary>
/// 配置接口
/// </summary>
public interface IConfig
{
    /// <summary>
    /// 模块编码
    /// </summary>
    string ModuleCode { get; }
}