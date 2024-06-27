using Microsoft.EntityFrameworkCore;
using OpenModular.DDD.Core.Uow;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;
using OpenModular.Persistence;

namespace OpenModular.Module.UAP.Core.Infrastructure.Persistence.Repositories;

internal class DataSeedingHistoryRepository(IUnitOfWork unitOfWork) : RepositoryAbstract<DataSeedingHistory, DataSeedingHistoryId, UAPDbContext>(unitOfWork), IDataSeedingHistoryRepository
{
    public async Task<int> GetLastVersionAsync(string moduleCode)
    {
        return await DbContext.DataSeedingHistories.Where(m => m.ModuleCode == moduleCode).MaxAsync(m => m.Version);
    }
}