using OpenModular.Module.DataSeeding;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.DataSeeding
{
    public abstract class UAPDataSeedingDefinition : IDataSeedingDefinition<UAPDbContext>
    {
        public abstract Task Define(UAPDbContext dbContext);
    }
}
