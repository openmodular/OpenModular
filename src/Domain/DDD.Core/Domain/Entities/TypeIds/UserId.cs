namespace OpenModular.DDD.Core.Domain.Entities.TypeIds;

/// <summary>
/// 用户标识
/// </summary>
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