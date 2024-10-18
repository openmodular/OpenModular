using OpenModular.Configuration.Abstractions;

namespace OpenModular.Module.Abstractions;

/// <summary>
/// 模块描述符
/// </summary>
public interface IModuleDescriptor
{
    /// <summary>
    /// 模块对象
    /// </summary>
    IModule Module { get; }

    /// <summary>
    /// 配置类描述符
    /// </summary>
    IConfigDescriptor Config { get; }

    /// <summary>
    /// 模块程序集
    /// </summary>
    IModuleAssemblies Assemblies { get; }
}