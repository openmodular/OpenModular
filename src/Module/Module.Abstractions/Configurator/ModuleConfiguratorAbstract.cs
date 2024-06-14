namespace OpenModular.Module.Abstractions.Configurator;

public abstract class ModuleConfiguratorAbstract : IModuleConfigurator
{
    public virtual void PreConfigureService(IModuleConfiguratorContext context)
    {
    }

    public virtual void ConfigureService(IModuleConfiguratorContext context)
    {
    }

    public virtual void PostConfigureService(IModuleConfiguratorContext context)
    {
    }
}