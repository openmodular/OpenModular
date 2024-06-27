using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

public class DataSeedingHistory : AggregateRoot<DataSeedingHistoryId>
{
    public string ModuleCode { get; }

    public int Version { get; }

    public DataSeedingHistory()
    {

    }

    private DataSeedingHistory(string moduleCode, int version) : base(new DataSeedingHistoryId())
    {
        Check.NotNullOrWhiteSpace(moduleCode, nameof(moduleCode));

        ModuleCode = moduleCode;
        Version = version;
    }

    public static DataSeedingHistory Create(string moduleCode, int version)
    {
        return new DataSeedingHistory(moduleCode, version);
    }
}