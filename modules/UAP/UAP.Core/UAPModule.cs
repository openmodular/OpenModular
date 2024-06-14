using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core;

public class UAPModule() : ModuleAbstract(UAPConstants.ModuleId, UAPConstants.ModuleCode)
{
    public override void ConfigureService(IModuleConfigureContext context)
    {
        context.Services.AddOpenModularDbContext<UAPDbContext>();
    }
}