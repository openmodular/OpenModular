using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Conventions;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

internal class AccountGetQueryHandler : QueryHandler<AccountGetQuery, AccountDto>
{
    public override Task<AccountDto> ExecuteAsync(AccountGetQuery request, CancellationToken cancellationToken)
    {
        throw new UAPBusinessException(UAPErrorCode.Account_Deleted);
    }
}