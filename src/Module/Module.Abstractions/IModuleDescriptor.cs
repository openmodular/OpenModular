namespace OpenModular.Module.Abstractions;

/// <summary>
/// 模块描述符接口
/// </summary>
public interface IModuleDescriptor
{
    IModule Module { get; }
}