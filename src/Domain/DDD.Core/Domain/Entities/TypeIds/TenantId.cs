namespace OpenModular.DDD.Core.Domain.Entities.TypeIds;

/// <summary>
/// 租户标识
/// </summary>
public class TenantId : TypedIdValueBase
{
    public TenantId()
    {

    }

    public TenantId(string id) : base(id)
    {

    }

    public TenantId(Guid id) : base(id)
    {

    }
}