using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Roles;

public sealed class RoleId : TypedIdValueBase
{
    public RoleId()
    {

    }

    public RoleId(string id) : base(id)
    {

    }

    public RoleId(Guid id) : base(id)
    {

    }
}