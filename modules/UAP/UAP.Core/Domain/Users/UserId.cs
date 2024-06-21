using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.UAP.Core.Domain.Users;

public sealed class UserId : TypedIdValueBase
{
    public UserId()
    {

    }

    public UserId(string id) : base(id)
    {

    }

    public UserId(Guid id) : base(id)
    {

    }
}