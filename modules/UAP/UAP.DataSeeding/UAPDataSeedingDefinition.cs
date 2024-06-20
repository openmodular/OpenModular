using OpenModular.Module.UAP.Core;
using OpenModular.Persistence.DataSeeding.Builder;

namespace OpenModular.Module.UAP.DataSeeding
{
    public abstract class UAPDataSeedingDefinition : DataSeedingDefinitionAbstract
    {
        public override string Module => UAPConstants.ModuleCode;
    }
}
