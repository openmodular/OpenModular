using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

internal class AccountGetQueryHandler : QueryHandler<AccountGetQuery, AccountDto>
{
    public override Task<AccountDto> ExecuteAsync(AccountGetQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}