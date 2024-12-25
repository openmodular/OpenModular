namespace OpenModular.Module.Blazor;

internal class ModuleBlazorDescriptor : IModuleBlazorDescriptor
{
    public ModuleBlazorDescriptor(IModuleBlazor moduleBlazor)
    {
        ModuleBlazor = moduleBlazor;
    }

    public IModuleBlazor ModuleBlazor { get; }
}