using OpenModular.Configuration.Abstractions;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

internal class ModuleDescriptor : IModuleDescriptor
{
    public ModuleDescriptor(IModule module)
    {
        Module = module;

        LoadConfigurator();
    }

    public IModule Module { get; }

    public IConfigDescriptor Config { get; set; }

    public IModuleConfigurator Configurator { get; set; }

    /// <summary>
    /// 加载模块配置器
    /// </summary>
    private void LoadConfigurator()
    {
        var coreAssembly = Module.GetType().Assembly;

        var moduleConfiguratorType = coreAssembly.GetTypes().FirstOrDefault(m =>
            typeof(IModuleConfigurator).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract);

        if (moduleConfiguratorType != null)
        {
            Configurator = (IModuleConfigurator)Activator.CreateInstance(moduleConfiguratorType)!;
        }
    }
}