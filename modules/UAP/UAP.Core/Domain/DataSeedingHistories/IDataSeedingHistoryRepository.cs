using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

internal interface IDataSeedingHistoryRepository : IRepository<DataSeedingHistory, DataSeedingHistoryId>
{
    /// <summary>
    /// 查询指定模块的最后更新版本
    /// </summary>
    /// <param name="moduleCode"></param>
    /// <returns></returns>
    Task<int> GetLastVersionAsync(string moduleCode);
}