using System.Reflection;

namespace OpenModular.Module.Abstractions;

/// <summary>
/// 模块程序集
/// </summary>
public interface IModuleAssemblies
{
    public Assembly Core { get; set; }

    public Assembly? Web { get; set; }

    public Assembly? Api { get; set; }
}