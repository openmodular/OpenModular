namespace OpenModular.DDD.Core.Domain.Entities.TypeIds;

/// <summary>
/// 账户标识
/// </summary>
public sealed class AccountId : TypedIdValueBase
{
    public AccountId()
    {

    }

    public AccountId(string id) : base(id)
    {

    }

    public AccountId(Guid id) : base(id)
    {

    }
}