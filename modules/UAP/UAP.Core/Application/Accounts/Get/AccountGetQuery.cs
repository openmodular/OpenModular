using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

public class AccountGetQuery : QueryBase<AccountDto>
{
    public AccountId AccountId { get; set; }
}