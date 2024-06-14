using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

internal class ModuleDescriptor : IModuleDescriptor
{
    public ModuleDescriptor(IModule module)
    {
        Module = module;
    }

    public IModule Module { get; }
}