using Microsoft.EntityFrameworkCore;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class DataSeedingHistoryRepository(IDbContextProvider<UAPDbContext> provider) : RepositoryAbstract<DataSeedingHistory, DataSeedingHistoryId, UAPDbContext>(provider), IDataSeedingHistoryRepository
{
    public async Task<int> GetLastVersionAsync(string moduleCode)
    {
        var query = await GetQueryableAsync();
        return await query.Where(m => m.ModuleCode == moduleCode).MaxAsync(m => m.Version);
    }
}