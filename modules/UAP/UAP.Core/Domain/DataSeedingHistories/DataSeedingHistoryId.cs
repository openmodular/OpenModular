using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.DataSeedingHistories;

public sealed class DataSeedingHistoryId : TypedIdValueBase
{
    public DataSeedingHistoryId()
    {

    }

    public DataSeedingHistoryId(string id) : base(id)
    {

    }

    public DataSeedingHistoryId(Guid id) : base(id)
    {

    }
}