using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Organizations;

public class OrganizationId : TypedIdValueBase
{
    public OrganizationId()
    {
    }

    public OrganizationId(string id) : base(id)
    {
    }

    public OrganizationId(Guid id) : base(id)
    {
    }
}