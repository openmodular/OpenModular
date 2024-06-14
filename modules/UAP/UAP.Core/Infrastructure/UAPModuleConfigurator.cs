using OpenModular.Module.Abstractions.Configurator;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure;

internal class UAPModuleConfigurator : ModuleConfiguratorAbstract
{
    public override void ConfigureService(IModuleConfiguratorContext context)
    {
        context.Services.AddOpenModularDbContext<UAPDbContext>();
    }
}