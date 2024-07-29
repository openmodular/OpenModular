using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Web;

public abstract class ModuleWebAbstract<TModule> : IModuleWeb where TModule : IModule, new ()
{
    public IModule Module => Activator.CreateInstance<TModule>();
}