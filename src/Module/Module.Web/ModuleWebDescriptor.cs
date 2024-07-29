namespace OpenModular.Module.Web;

internal class ModuleWebDescriptor : IModuleWebDescriptor
{
    public ModuleWebDescriptor(IModuleWeb moduleWeb)
    {
        ModuleWeb = moduleWeb;

        LoadConfigurator();
    }

    public IModuleWeb ModuleWeb { get; }

    public IModuleWebConfigurator Configurator { get; set; }

    private void LoadConfigurator()
    {
        var coreAssembly = ModuleWeb.GetType().Assembly;

        var moduleConfiguratorType = coreAssembly.GetTypes().FirstOrDefault(m =>
            typeof(IModuleWebConfigurator).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract);

        if (moduleConfiguratorType != null)
        {
            Configurator = (IModuleWebConfigurator)Activator.CreateInstance(moduleConfiguratorType)!;
        }
    }
}