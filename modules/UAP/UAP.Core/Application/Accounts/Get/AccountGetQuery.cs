using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

/// <summary>
/// 根据指定标识查询账户信息
/// </summary>
public class AccountGetQuery : Query<AccountDto>
{
    public AccountGetQuery(AccountId id)
    {
        Id = id;
    }

    public AccountId Id { get; set; }
}