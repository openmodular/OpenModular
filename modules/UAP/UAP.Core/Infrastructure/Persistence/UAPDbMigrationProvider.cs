using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence;

internal class UAPDbMigrationProvider(UAPDbContext context) : DbMigrationProviderAbstract<UAPDbContext>(context);