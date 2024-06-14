using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Api;

/// <summary>
/// 定义模块API的抽象类
/// </summary>
/// <typeparam name="TModule"></typeparam>
public abstract class ModuleApiAbstract<TModule> : IModuleApi where TModule : IModule, new()
{
    public IModule Module => Activator.CreateInstance<TModule>();
}