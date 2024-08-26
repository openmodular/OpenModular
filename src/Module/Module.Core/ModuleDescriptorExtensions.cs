using OpenModular.Config.Abstractions;
using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

public static class ModuleDescriptorExtensions
{
    /// <summary>
    /// 设置配置类描述符
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="configDescriptor"></param>
    /// <returns></returns>
    public static IModuleDescriptor SetConfigDescriptor(this IModuleDescriptor descriptor, IConfigDescriptor configDescriptor)
    {
        (descriptor as ModuleDescriptor)!.Config = configDescriptor;

        return descriptor;
    }
}