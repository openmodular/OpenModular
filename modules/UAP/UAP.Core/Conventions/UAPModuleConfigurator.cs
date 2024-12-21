using Microsoft.Extensions.DependencyInjection;
using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.Core.Conventions;

internal class UAPModuleConfigurator : ModuleConfiguratorAbstract
{
    public override void ConfigureService(IModuleConfiguratorContext context)
    {
        context.Services.AddOpenModularDbContext<UAPDbContext>();
        context.Services.AddDataSeeding<UAPDbContext>();
    }
}