using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

public class DataSeedingHistory : AggregateRoot<DataSeedingHistoryId>
{
    private string _moduleCode;

    public string ModuleCode
    {
        get => _moduleCode;
        set
        {
            Check.NullOrWhiteSpace(value, nameof(ModuleCode));
            _moduleCode = value;
        }
    }

    public int Version { get; set; }

    public DataSeedingHistory()
    {
        //for ef
    }

    private DataSeedingHistory(string moduleCode, int version) : base(new DataSeedingHistoryId())
    {
        ModuleCode = moduleCode;
        Version = version;
    }

    public static DataSeedingHistory Create(string moduleCode, int version)
    {
        return new DataSeedingHistory(moduleCode, version);
    }
}