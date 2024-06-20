using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;

namespace OpenModular.Module.UAP.Core;

internal class UAPModuleConfigurator : ModuleConfiguratorAbstract
{
    public override void ConfigureService(IModuleConfiguratorContext context)
    {
        context.Services.AddOpenModularDbContext<UAPDbContext>();
        context.Services.AddDataSeeding<UAPDbContext>();
    }
}