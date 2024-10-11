using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

internal class AccountGetQueryHandler : IQueryHandler<AccountGetQuery, AccountDto>
{
    public Task<AccountDto> Handle(AccountGetQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}