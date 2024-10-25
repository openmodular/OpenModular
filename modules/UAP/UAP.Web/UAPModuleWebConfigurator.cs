using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Abstractions;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web;

public class UAPModuleWebConfigurator : ModuleWebConfiguratorAbstract
{
    public override void PreConfigureService(IModuleConfiguratorContext context)
    {
        context.Services.AddUAPTypedIdJsonConverters();
    }
}