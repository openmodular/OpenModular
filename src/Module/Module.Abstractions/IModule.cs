namespace OpenModular.Module.Abstractions;

/// <summary>
/// 用于定义模块的接口
/// </summary>
public interface IModule
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    int Id { get; }

    /// <summary>
    /// 模块唯一编码
    /// </summary>
    string Code { get; }
}