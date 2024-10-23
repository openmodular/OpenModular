using OpenModular.Persistence.DataSeeding;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

namespace OpenModular.Module.UAP.Core.Infrastructure;

internal class DataSeedingHistoryStorage : IDataSeedingHistoryStorage, ITransientDependency
{
    private readonly IDataSeedingHistoryRepository _repository;

    public DataSeedingHistoryStorage(IDataSeedingHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetLastVersionAsync(string moduleCode)
    {
        try
        {
            return await _repository.GetLastVersionAsync(moduleCode);
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }

    public async Task InsertVersionAsync(string moduleCode, int version)
    {
        await _repository.InsertAsync(DataSeedingHistory.Create(moduleCode, version));
    }
}