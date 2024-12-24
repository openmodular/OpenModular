namespace OpenModular.Module.Blazor;

/// <summary>
/// Interface for module blazor project.
/// </summary>
public interface IModuleBlazor
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    int Id { get; }

    /// <summary>
    /// 模块唯一编码
    /// </summary>
    string Code { get; }

    /// <summary>
    /// 版本号
    /// </summary>
    string Version { get; }
}